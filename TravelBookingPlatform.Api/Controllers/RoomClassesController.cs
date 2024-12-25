using System.Text.Json;
using Asp.Versioning;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TravelBookingPlatform.Api.Dtos.Images;
using TravelBookingPlatform.Api.Dtos.RoomClasses;
using TravelBookingPlatform.Api.Extensions;
using TravelBookingPlatform.Application.RoomClasses.AddImageToGallery;
using TravelBookingPlatform.Application.RoomClasses.Create;
using TravelBookingPlatform.Application.RoomClasses.Delete;
using TravelBookingPlatform.Application.RoomClasses.GetForManagement;
using TravelBookingPlatform.Application.RoomClasses.Update;
using TravelBookingPlatform.Domain.Enums;

namespace TravelBookingPlatform.Api.Controllers;

[ApiController]
[Route("api/room-classes")]
[ApiVersion("1.0")]
[Authorize(Roles = nameof(UserRole.Admin))]
public class RoomClassesController : ControllerBase
{
  private readonly ISender _mediator;
  private readonly IMapper _mapper;

  public RoomClassesController(ISender mediator, IMapper mapper)
  {
    _mediator = mediator;
    _mapper = mapper;
  }

  [ProducesResponseType(StatusCodes.Status200OK)]
  [ProducesResponseType(StatusCodes.Status400BadRequest)]
  [ProducesResponseType(StatusCodes.Status401Unauthorized)]
  [ProducesResponseType(StatusCodes.Status403Forbidden)]
  [HttpGet]
  public async Task<ActionResult<IEnumerable<RoomClassForManagementResponse>>> GetRoomClassesForManagement(
    [FromQuery] RoomClassesGetRequest roomClassesGetRequest,
    CancellationToken cancellationToken)
  {
    var query = _mapper.Map<GetRoomClassesForManagementQuery>(roomClassesGetRequest);

    var roomClasses = await _mediator.Send(query, cancellationToken);

    Response.Headers.AddPaginationMetadata(roomClasses.PaginationMetadata);

    return Ok(roomClasses.Items);
  }

  [ProducesResponseType(StatusCodes.Status201Created)]
  [ProducesResponseType(StatusCodes.Status400BadRequest)]
  [ProducesResponseType(StatusCodes.Status401Unauthorized)]
  [ProducesResponseType(StatusCodes.Status403Forbidden)]
  [ProducesResponseType(StatusCodes.Status404NotFound)]
  [ProducesResponseType(StatusCodes.Status409Conflict)]
  [HttpPost]
  public async Task<IActionResult> CreateRoomClass(
    RoomClassCreationRequest roomClassCreationRequest,
    CancellationToken cancellationToken)
  {
    var command = _mapper.Map<CreateRoomClassCommand>(roomClassCreationRequest);

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
  public async Task<IActionResult> UpdateRoomClass(
    Guid id,
    RoomClassUpdateRequest roomClassUpdateRequest,
    CancellationToken cancellationToken)
  {
    var command = new UpdateRoomClassCommand { RoomClassId = id };
    _mapper.Map(roomClassUpdateRequest, command);

    await _mediator.Send(command, cancellationToken);

    return NoContent();
  }

  [ProducesResponseType(StatusCodes.Status204NoContent)]
  [ProducesResponseType(StatusCodes.Status401Unauthorized)]
  [ProducesResponseType(StatusCodes.Status403Forbidden)]
  [ProducesResponseType(StatusCodes.Status404NotFound)]
  [ProducesResponseType(StatusCodes.Status409Conflict)]
  [HttpDelete("{id:guid}")]
  public async Task<IActionResult> DeleteRoomClass(
    Guid id,
    CancellationToken cancellationToken)
  {
    var command = new DeleteRoomClassCommand { RoomClassId = id };

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
    var command = new AddImageToRoomClassGalleryCommand { RoomClassId = id };
    _mapper.Map(imageCreationRequest, command);

    await _mediator.Send(command, cancellationToken);

    return NoContent();
  }
}