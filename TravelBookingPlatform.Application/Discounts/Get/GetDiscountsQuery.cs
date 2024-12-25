using MediatR;
using TravelBookingPlatform.Application.Discounts.GetById;
using TravelBookingPlatform.Domain.Enums;
using TravelBookingPlatform.Domain.Model;

namespace TravelBookingPlatform.Application.Discounts.Get;

public class GetDiscountsQuery : IRequest<PaginatedResult<DiscountResponse>>
{
  public Guid RoomClassId { get; init; }
  public SortOrder? SortOrder { get; init; }
  public string? SortColumn { get; init; }
  public int PageNumber { get; init; }
  public int PageSize { get; init; }
}