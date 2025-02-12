using System.Text.Json;
using Asp.Versioning;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TravelBookingPlatform.Api.Dtos.Hotels;
using TravelBookingPlatform.Api.Dtos.Images;
using TravelBookingPlatform.Api.Dtos.RoomClasses;
using TravelBookingPlatform.Api.Extensions;
using TravelBookingPlatform.Application.Hotels.AddToGallery;
using TravelBookingPlatform.Application.Hotels.Create;
using TravelBookingPlatform.Application.Hotels.Delete;
using TravelBookingPlatform.Application.Hotels.GetFeaturedDeals;
using TravelBookingPlatform.Application.Hotels.GetForGuestById;
using TravelBookingPlatform.Application.Hotels.GetForManagement;
using TravelBookingPlatform.Application.Hotels.Search;
using TravelBookingPlatform.Application.Hotels.SetThumbnail;
using TravelBookingPlatform.Application.Hotels.Update;
using TravelBookingPlatform.Application.RoomClasses.GetByHotelIdForGuest;
using TravelBookingPlatform.Domain.Enums;

namespace TravelBookingPlatform.Api.Controllers;

[ApiController]
[Route("api/hotels")]
[ApiVersion("1.0")]
[Authorize(Roles = nameof(UserRole.Admin))]
public class HotelsController : ControllerBase
{
  private readonly ISender _mediator;
  private readonly IMapper _mapper;

  public HotelsController(ISender mediator, IMapper mapper)
  {
    _mediator = mediator;
    _mapper = mapper;
  }

  [ProducesResponseType(typeof(IEnumerable<HotelForManagementResponse>), StatusCodes.Status200OK)]
  [ProducesResponseType(StatusCodes.Status400BadRequest)]
  [HttpGet]
  public async Task<ActionResult<IEnumerable<HotelForManagementResponse>>> GetHotelsForManagement(
    [FromQuery] HotelsGetRequest hotelsGetRequest,
    CancellationToken cancellationToken)
  {
    if (hotelsGetRequest is null)
    {
      return BadRequest("Hotels request cannot be null.");
    }

    var query = _mapper.Map<GetHotelsForManagementQuery>(hotelsGetRequest);

    var hotels = await _mediator.Send(query, cancellationToken);

    Response.Headers.AddPaginationMetadata(hotels.PaginationMetadata);

    return Ok(hotels.Items);
  }

  [ProducesResponseType(typeof(IEnumerable<HotelSearchResultResponse>), StatusCodes.Status200OK)]
  [ProducesResponseType(StatusCodes.Status400BadRequest)]
  [HttpGet("search")]
  [AllowAnonymous]
  public async Task<ActionResult<IEnumerable<HotelSearchResultResponse>>> SearchAndFilterHotels(
    [FromQuery] HotelSearchRequest hotelSearchRequest,
    CancellationToken cancellationToken = default)
  {
    if (hotelSearchRequest is null)
    {
      return BadRequest("Search request cannot be null.");
    }

    var query = _mapper.Map<SearchForHotelsQuery>(hotelSearchRequest);

    var hotels = await _mediator.Send(query, cancellationToken);

    Response.Headers.AddPaginationMetadata(hotels.PaginationMetadata);

    return Ok(hotels.Items);
  }

  [ProducesResponseType(typeof(IEnumerable<HotelFeaturedDealResponse>), StatusCodes.Status200OK)]
  [ProducesResponseType(StatusCodes.Status400BadRequest)]
  [HttpGet("featured-deals")]
  [AllowAnonymous]
  public async Task<ActionResult<IEnumerable<HotelFeaturedDealResponse>>> GetFeaturedDeals(
    [FromQuery] HotelFeaturedDealsGetRequest hotelFeaturedDealsGetRequest,
    CancellationToken cancellationToken = default)
  {
    if (hotelFeaturedDealsGetRequest is null)
    {
      return BadRequest("Featured deals request cannot be null.");
    }

    var query = _mapper.Map<GetHotelFeaturedDealsQuery>(hotelFeaturedDealsGetRequest);

    var featuredDeals = await _mediator.Send(query, cancellationToken);

    return Ok(featuredDeals);
  }

  [ProducesResponseType(typeof(HotelForGuestResponse), StatusCodes.Status200OK)]
  [ProducesResponseType(StatusCodes.Status404NotFound)]
  [HttpGet("{id:guid}")]
  [AllowAnonymous]
  public async Task<ActionResult<HotelForGuestResponse>> GetHotelForGuest(Guid id,
    CancellationToken cancellationToken = default)
  {
    var query = new GetHotelForGuestByIdQuery { HotelId = id };

    var hotel = await _mediator.Send(query, cancellationToken);

    if (hotel is null)
    {
      return NotFound($"Hotel with ID {id} not found.");
    }

    return Ok(hotel);
  }

  [ProducesResponseType(typeof(IEnumerable<RoomClassForGuestResponse>), StatusCodes.Status200OK)]
  [ProducesResponseType(StatusCodes.Status404NotFound)]
  [HttpGet("{id:guid}/room-classes")]
  [AllowAnonymous]
  public async Task<ActionResult<IEnumerable<RoomClassForGuestResponse>>> GetRoomClassesForGuests(
    Guid id,
    [FromQuery] GetRoomClassesForGuestRequest getRoomClassesForGuestRequest,
    CancellationToken cancellationToken = default)
  {
    if (getRoomClassesForGuestRequest is null)
    {
      return BadRequest("Room classes request cannot be null.");
    }

    var query = new GetRoomClassesByHotelIdForGuestQuery { HotelId = id };
    _mapper.Map(getRoomClassesForGuestRequest, query);

    var roomClasses = await _mediator.Send(query, cancellationToken);

    Response.Headers.AddPaginationMetadata(roomClasses.PaginationMetadata);

    return Ok(roomClasses.Items);
  }

  [ProducesResponseType(StatusCodes.Status201Created)]
  [ProducesResponseType(StatusCodes.Status400BadRequest)]
  [ProducesResponseType(StatusCodes.Status401Unauthorized)]
  [ProducesResponseType(StatusCodes.Status403Forbidden)]
  [ProducesResponseType(StatusCodes.Status409Conflict)]
  [HttpPost]
  public async Task<IActionResult> CreateHotel(
    HotelCreationRequest hotelCreationRequest,
    CancellationToken cancellationToken)
  {
    if (hotelCreationRequest is null)
    {
      return BadRequest("Hotel creation request cannot be null.");
    }

    var command = _mapper.Map<CreateHotelCommand>(hotelCreationRequest);

    await _mediator.Send(command, cancellationToken);

    return Created();
  }

  [ProducesResponseType(StatusCodes.Status204NoContent)]
  [ProducesResponseType(StatusCodes.Status400BadRequest)]
  [ProducesResponseType(StatusCodes.Status401Unauthorized)]
  [ProducesResponseType(StatusCodes.Status403Forbidden)]
  [ProducesResponseType(StatusCodes.Status404NotFound)]
  [ProducesResponseType(StatusCodes.Status409Conflict)]
  [HttpPut("{id:guid}")]
  public async Task<IActionResult> UpdateHotel(
    Guid id,
    HotelUpdateRequest hotelUpdateRequest,
    CancellationToken cancellationToken)
  {
    if (hotelUpdateRequest is null)
    {
      return BadRequest("Hotel update request cannot be null.");
    }

    var command = new UpdateHotelCommand { HotelId = id };
    _mapper.Map(hotelUpdateRequest, command);

    await _mediator.Send(command, cancellationToken);

    return NoContent();
  }

  [ProducesResponseType(StatusCodes.Status204NoContent)]
  [ProducesResponseType(StatusCodes.Status401Unauthorized)]
  [ProducesResponseType(StatusCodes.Status403Forbidden)]
  [ProducesResponseType(StatusCodes.Status404NotFound)]
  [ProducesResponseType(StatusCodes.Status409Conflict)]
  [HttpDelete("{id:guid}")]
  public async Task<IActionResult> DeleteHotel(
    Guid id,
    CancellationToken cancellationToken)
  {
    var command = new DeleteHotelCommand { HotelId = id };

    await _mediator.Send(
      command,
      cancellationToken);

    return NoContent();
  }

  [ProducesResponseType(StatusCodes.Status204NoContent)]
  [ProducesResponseType(StatusCodes.Status400BadRequest)]
  [ProducesResponseType(StatusCodes.Status401Unauthorized)]
  [ProducesResponseType(StatusCodes.Status403Forbidden)]
  [ProducesResponseType(StatusCodes.Status404NotFound)]
  [HttpPut("{id:guid}/thumbnail")]
  public async Task<IActionResult> SetHotelThumbnail(
    Guid id,
    [FromForm] ImageCreationRequest imageCreationRequest,
    CancellationToken cancellationToken)
  {
    if (imageCreationRequest is null)
    {
      return BadRequest("Thumbnail image request cannot be null.");
    }

    var command = new SetHotelThumbnailCommand { HotelId = id };
    _mapper.Map(imageCreationRequest, command);
    
    await _mediator.Send(command, cancellationToken);

    return NoContent();
  }

  [ProducesResponseType(StatusCodes.Status204NoContent)]
  [ProducesResponseType(StatusCodes.Status400BadRequest)]
  [ProducesResponseType(StatusCodes.Status401Unauthorized)]
  [ProducesResponseType(StatusCodes.Status403Forbidden)]
  [ProducesResponseType(StatusCodes.Status404NotFound)]
  [HttpPost("{id:guid}/gallery")]
  public async Task<IActionResult> AddImageToHotelGallery(
    Guid id,
    [FromForm] ImageCreationRequest imageCreationRequest,
    CancellationToken cancellationToken)
  {
    if (imageCreationRequest is null)
    {
      return BadRequest("Image gallery request cannot be null.");
    }

    var command = new AddImageToHotelGalleryCommand { HotelId = id };
    _mapper.Map(imageCreationRequest, command);

    await _mediator.Send(command, cancellationToken);

    return NoContent();
  }
}