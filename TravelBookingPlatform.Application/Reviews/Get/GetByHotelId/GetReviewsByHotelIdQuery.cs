using MediatR;
using TravelBookingPlatform.Application.Reviews.Common;
using TravelBookingPlatform.Domain.Enums;
using TravelBookingPlatform.Domain.Model;

namespace TravelBookingPlatform.Application.Reviews.GetByHotelId;

public class GetReviewsByHotelIdQuery : IRequest<PaginatedResult<ReviewResponse>>
{
  public Guid HotelId { get; init; }
  public SortOrder? SortOrder { get; init; }
  public string? SortColumn { get; init; }
  public int PageNumber { get; init; }
  public int PageSize { get; init; }
}