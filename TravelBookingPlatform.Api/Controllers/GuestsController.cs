using System.Security.Claims;
using Asp.Versioning;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TravelBookingPlatform.Api.Dtos.Hotels;
using TravelBookingPlatform.Application.Hotels.GetRecentlyVisited;
using TravelBookingPlatform.Domain.Enums;

namespace TravelBookingPlatform.Api.Controllers;

[ApiController]
[Route("api/user")]
[ApiVersion("1.0")]
[Authorize(Roles = nameof(UserRole.Guest))]
public class GuestsController : ControllerBase
{
  private readonly ISender _mediator;
  private readonly IMapper _mapper;

  public GuestsController(ISender mediator, IMapper mapper)
  {
    _mediator = mediator;
    _mapper = mapper;
  }

  [ProducesResponseType(typeof(IEnumerable<RecentlyVisitedHotelResponse>), StatusCodes.Status200OK)]
  [ProducesResponseType(StatusCodes.Status400BadRequest)]
  [ProducesResponseType(StatusCodes.Status401Unauthorized)]
  [ProducesResponseType(StatusCodes.Status403Forbidden)]
  [HttpGet("recently-visited-hotels")]
  public async Task<ActionResult<IEnumerable<RecentlyVisitedHotelResponse>>> GetRecentlyVisitedHotels(
    [FromQuery] RecentlyVisitedHotelsGetRequest recentlyVisitedHotelsGetRequest,
    CancellationToken cancellationToken)
  {
    if (recentlyVisitedHotelsGetRequest is null)
    {
      return BadRequest("Recently visited hotels request cannot be null.");
    }

    var guestId = Guid.Parse(
      User.FindFirstValue(ClaimTypes.NameIdentifier)
      ?? throw new ArgumentNullException());

    var query = new GetRecentlyVisitedHotelsForGuestQuery { GuestId = guestId };
    _mapper.Map(recentlyVisitedHotelsGetRequest, query);

    var hotels = await _mediator.Send(query, cancellationToken);

    return Ok(hotels);
  }
}