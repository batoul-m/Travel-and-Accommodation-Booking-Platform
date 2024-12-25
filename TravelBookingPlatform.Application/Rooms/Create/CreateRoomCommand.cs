using MediatR;

namespace TravelBookingPlatform.Application.Rooms.Create;

public class CreateRoomCommand : IRequest<Guid>
{
  public Guid RoomClassId { get; init; }
  public string Number { get; init; }
}