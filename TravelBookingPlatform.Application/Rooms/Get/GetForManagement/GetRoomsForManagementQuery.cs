using MediatR;
using TravelBookingPlatform.Domain.Enums;
using TravelBookingPlatform.Domain.Model;

namespace TravelBookingPlatform.Application.Rooms.GetForManagement;

public class GetRoomsForManagementQuery : IRequest<PaginatedResult<RoomForManagementResponse>>
{
  public Guid RoomClassId { get; init; }
  public string? SearchTerm { get; init; }
  public SortOrder? SortOrder { get; init; }
  public string? SortColumn { get; init; }
  public int PageNumber { get; init; }
  public int PageSize { get; init; }
}