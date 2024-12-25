using MediatR;
using TravelBookingPlatform.Domain.Enums;
using TravelBookingPlatform.Domain.Model;

namespace TravelBookingPlatform.Application.RoomClasses.GetByHotelIdForGuest;

public class GetRoomClassesByHotelIdForGuestQuery : IRequest<PaginatedResult<RoomClassForGuestResponse>>
{
  public Guid HotelId { get; init; }
  public int PageNumber { get; init; }
  public int PageSize { get; init; }
  public SortOrder? SortOrder { get; init; }
  public string? SortColumn { get; init; }
}