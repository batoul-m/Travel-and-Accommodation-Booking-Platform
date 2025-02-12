using System.Text.Json;
using Asp.Versioning;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TravelBookingPlatform.Api.Dtos.Rooms;
using TravelBookingPlatform.Api.Extensions;
using TravelBookingPlatform.Application.Rooms.Create;
using TravelBookingPlatform.Application.Rooms.Delete;
using TravelBookingPlatform.Application.Rooms.GetByRoomClassIdForGuest;
using TravelBookingPlatform.Application.Rooms.GetForManagement;
using TravelBookingPlatform.Application.Rooms.Update;
using TravelBookingPlatform.Domain;
using TravelBookingPlatform.Domain.Enums;

namespace TravelBookingPlatform.Api.Controllers;

[ApiController]
[Route("api/room-classes/{roomClassId:guid}/rooms")]
[ApiVersion("1.0")]
[Authorize(Roles = nameof(UserRole.Admin))]
public class RoomsController : ControllerBase
{
  private readonly ISender _mediator;
  private readonly IMapper _mapper;

  public RoomsController(ISender mediator, IMapper mapper)
  {
    _mediator = mediator;
    _mapper = mapper;
  }

  [ProducesResponseType(StatusCodes.Status200OK)]
  [ProducesResponseType(StatusCodes.Status400BadRequest)]
  [ProducesResponseType(StatusCodes.Status401Unauthorized)]
  [ProducesResponseType(StatusCodes.Status403Forbidden)]
  [ProducesResponseType(StatusCodes.Status404NotFound)]
  [HttpGet]
  public async Task<ActionResult<IEnumerable<RoomForManagementResponse>>> GetRoomsForManagement(
    Guid roomClassId,
    [FromQuery] RoomsGetRequest roomsGetRequest,
    CancellationToken cancellationToken)
  {
    var query = new GetRoomsForManagementQuery { RoomClassId = roomClassId };
    _mapper.Map(roomsGetRequest, query);

    var rooms = await _mediator.Send(query, cancellationToken);

    Response.Headers.AddPaginationMetadata(rooms.PaginationMetadata);

    return Ok(rooms.Items);
  }

  [ProducesResponseType(StatusCodes.Status200OK)]
  [ProducesResponseType(StatusCodes.Status400BadRequest)]
  [ProducesResponseType(StatusCodes.Status404NotFound)]
  [HttpGet("available")]
  [AllowAnonymous]
  public async Task<ActionResult<IEnumerable<RoomForGuestResponse>>> GetRoomsForGuests(
    Guid roomClassId,
    [FromQuery] RoomsForGuestsGetRequest roomsForGuestsGetRequest,
    CancellationToken cancellationToken)
  {
    var query = new GetRoomsByRoomClassIdForGuestsQuery { RoomClassId = roomClassId };
    _mapper.Map(roomsForGuestsGetRequest, query);

    var rooms = await _mediator.Send(query, cancellationToken);

    Response.Headers.AddPaginationMetadata(rooms.PaginationMetadata);

    return Ok(rooms.Items);
  }

  [ProducesResponseType(StatusCodes.Status201Created)]
  [ProducesResponseType(StatusCodes.Status400BadRequest)]
  [ProducesResponseType(StatusCodes.Status401Unauthorized)]
  [ProducesResponseType(StatusCodes.Status403Forbidden)]
  [ProducesResponseType(StatusCodes.Status404NotFound)]
  [ProducesResponseType(StatusCodes.Status409Conflict)]
  [HttpPost]
  public async Task<IActionResult> CreateRoomInRoomClass(
    Guid roomClassId,
    RoomCreationRequest roomCreationRequest,
    CancellationToken cancellationToken)
  {
    var command = new CreateRoomCommand { RoomClassId = roomClassId };
    _mapper.Map(roomCreationRequest, command);

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
  public async Task<IActionResult> UpdateRoomInRoomClass(
    Guid roomClassId, Guid id,
    RoomUpdateRequest roomUpdateRequest,
    CancellationToken cancellationToken)
  {
    var command = new UpdateRoomCommand
    {
      RoomClassId = roomClassId,
      RoomId = id
    };
    _mapper.Map(roomUpdateRequest, command);

    await _mediator.Send(command, cancellationToken);

    return NoContent();
  }

  [ProducesResponseType(StatusCodes.Status204NoContent)]
  [ProducesResponseType(StatusCodes.Status401Unauthorized)]
  [ProducesResponseType(StatusCodes.Status403Forbidden)]
  [ProducesResponseType(StatusCodes.Status404NotFound)]
  [ProducesResponseType(StatusCodes.Status409Conflict)]
  [HttpDelete("{id:guid}")]
  public async Task<IActionResult> DeleteRoomInRoomClass(Guid roomClassId,
    Guid id,
    CancellationToken cancellationToken = default)
  {
    var command = new DeleteRoomCommand
    {
      RoomClassId = roomClassId,
      RoomId = id
    };

    await _mediator.Send(command, cancellationToken);

    return NoContent();
  }
}