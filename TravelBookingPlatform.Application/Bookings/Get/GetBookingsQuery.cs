using MediatR;
using TravelBookingPlatform.Domain.Enums;
using TravelBookingPlatform.Domain.Model;
using TravelBookingPlatform.Application.Bookings.Common;

namespace TravelBookingPlatform.Application.Bookings;

public class GetBookingsQuery : IRequest<PaginatedResult<BookingResponse>>
{
  public int PageNumber { get; init; }
  public int PageSize { get; init; }
  public SortOrder? SortOrder { get; init; }
  public string? SortColumn { get; init; }
}