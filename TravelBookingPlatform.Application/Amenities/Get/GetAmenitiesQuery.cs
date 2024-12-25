using MediatR;
using TravelBookingPlatform.Application.Amenities;
using TravelBookingPlatform.Domain.Enums;
using TravelBookingPlatform.Domain.Model;

namespace TravelBookingPlatform.Application.Amenities;

public class GetAmenitiesQuery : IRequest<PaginatedResult<AmenityResponse>>
{
  public string? SearchTerm { get; init; }
  public SortOrder? SortOrder { get; init; }
  public string? SortColumn { get; init; }
  public int PageNumber { get; init; }
  public int PageSize { get; init; }
}