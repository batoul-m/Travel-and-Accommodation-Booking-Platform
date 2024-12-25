using MediatR;
using TravelBookingPlatform.Domain.Model;

namespace TravelBookingPlatform.Application.Rooms.GetByRoomClassIdForGuest;

public class GetRoomsByRoomClassIdForGuestsQuery : IRequest<PaginatedResult<RoomForGuestResponse>>
{
  public Guid RoomClassId { get; init; }
  public DateOnly CheckInDate { get; init; }
  public DateOnly CheckOutDate { get; init; }
  public int PageNumber { get; init; }
  public int PageSize { get; init; }
}