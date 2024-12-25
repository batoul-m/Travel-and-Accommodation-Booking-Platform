using MediatR;

namespace TravelBookingPlatform.Application.RoomClasses.Delete;

public class DeleteRoomClassCommand : IRequest
{
  public Guid RoomClassId { get; init; }
}