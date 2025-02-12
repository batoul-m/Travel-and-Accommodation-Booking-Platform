using System.Security.Claims;
using System.Text.Json;
using Asp.Versioning;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TravelBookingPlatform.Api.Dtos.Reviews;
using TravelBookingPlatform.Api.Extensions;
using TravelBookingPlatform.Application.Reviews.Common;
using TravelBookingPlatform.Application.Reviews.Create;
using TravelBookingPlatform.Application.Reviews.Delete;
using TravelBookingPlatform.Application.Reviews.GetByHotelId;
using TravelBookingPlatform.Application.Reviews.GetById;
using TravelBookingPlatform.Application.Reviews.Update;
using TravelBookingPlatform.Domain;
using TravelBookingPlatform.Domain.Enums;

namespace TravelBookingPlatform.Api.Controllers;

[ApiController]
[Route("api/hotels/{hotelId:guid}/reviews")]
[ApiVersion("1.0")]
[Authorize(Roles = nameof(UserRole.Guest))]
public class ReviewsController : ControllerBase
{
  private readonly ISender _mediator;
  private readonly IMapper _mapper;

  public ReviewsController(ISender mediator, IMapper mapper)
  {
    _mediator = mediator;
    _mapper = mapper;
  }

  [ProducesResponseType(StatusCodes.Status200OK)]
  [ProducesResponseType(StatusCodes.Status400BadRequest)]
  [ProducesResponseType(StatusCodes.Status404NotFound)]
  [HttpGet]
  [AllowAnonymous]
  public async Task<ActionResult<IEnumerable<ReviewResponse>>> GetReviewsForHotel(
    Guid hotelId,
    [FromQuery] ReviewsGetRequest reviewsGetRequest,
    CancellationToken cancellationToken)
  {
    var query = new GetReviewsByHotelIdQuery { HotelId = hotelId };
    _mapper.Map(reviewsGetRequest, query);

    var reviews = await _mediator.Send(query, cancellationToken);

    Response.Headers.AddPaginationMetadata(reviews.PaginationMetadata);

    return Ok(reviews.Items);
  }

  [ProducesResponseType(StatusCodes.Status404NotFound)]
  [HttpGet("{id:guid}")]
  [AllowAnonymous]
  public async Task<ActionResult<ReviewResponse>> GetReviewById(
    Guid hotelId, Guid id, CancellationToken cancellationToken = default)
  {
    var query = new GetReviewByIdQuery
    {
      HotelId = hotelId,
      ReviewId = id
    };

    var review = await _mediator.Send(query, cancellationToken);

    return Ok(review);
  }

  [ProducesResponseType(StatusCodes.Status201Created)]
  [ProducesResponseType(StatusCodes.Status400BadRequest)]
  [ProducesResponseType(StatusCodes.Status401Unauthorized)]
  [ProducesResponseType(StatusCodes.Status403Forbidden)]
  [ProducesResponseType(StatusCodes.Status404NotFound)]
  [ProducesResponseType(StatusCodes.Status409Conflict)]
  [HttpPost]
  public async Task<IActionResult> CreateReviewForHotel(Guid hotelId,
    ReviewCreationRequest reviewCreationRequest,
    CancellationToken cancellationToken)
  {
    var command = new CreateReviewCommand { HotelId = hotelId };
    _mapper.Map(reviewCreationRequest, command);

    var createdReview = await _mediator.Send(command, cancellationToken);
    
    return CreatedAtAction(nameof(GetReviewById), new { id = createdReview }, createdReview);
  }

  [ProducesResponseType(StatusCodes.Status204NoContent)]
  [ProducesResponseType(StatusCodes.Status400BadRequest)]
  [ProducesResponseType(StatusCodes.Status401Unauthorized)]
  [ProducesResponseType(StatusCodes.Status403Forbidden)]
  [ProducesResponseType(StatusCodes.Status404NotFound)]
  [HttpPut("{id:guid}")]
  public async Task<IActionResult> UpdateReviewForHotel(
    Guid hotelId, Guid id,
    ReviewUpdateRequest reviewUpdateRequest,
    CancellationToken cancellationToken)
  {
    var command = new UpdateReviewCommand { HotelId = hotelId, ReviewId = id };
    _mapper.Map(reviewUpdateRequest, command);

    await _mediator.Send(
      command,
      cancellationToken);

    return NoContent();
  }

  [ProducesResponseType(StatusCodes.Status204NoContent)]
  [ProducesResponseType(StatusCodes.Status401Unauthorized)]
  [ProducesResponseType(StatusCodes.Status403Forbidden)]
  [ProducesResponseType(StatusCodes.Status404NotFound)]
  [HttpDelete("{id:guid}")]
  public async Task<IActionResult> DeleteReviewForHotel(
    Guid hotelId, Guid id,
    CancellationToken cancellationToken)
  {
    var command = new DeleteReviewCommand { HotelId = hotelId, ReviewId = id };

    await _mediator.Send(
      command,
      cancellationToken);

    return NoContent();
  }
}