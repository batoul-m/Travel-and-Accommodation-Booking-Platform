﻿using MediatR;

namespace TravelBookingPlatform.Application.Rooms.Update;

public class UpdateRoomCommand : IRequest
{
  public Guid RoomClassId { get; init; }
  public Guid RoomId { get; init; }
  public string Number { get; init; }
}