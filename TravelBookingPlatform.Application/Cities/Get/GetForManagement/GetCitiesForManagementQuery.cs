using MediatR;
using TravelBookingPlatform.Domain.Enums;
using TravelBookingPlatform.Domain.Model;
using TravelBookingPlatform.Domain.Enums;

namespace TravelBookingPlatform.Application.Cities.GetForManagement;

public class GetCitiesForManagementQuery : IRequest<PaginatedResult<CityForManagementResponse>>
{
  public string? SearchTerm { get; init; }
  public SortOrder? SortOrder { get; init; }
  public string? SortColumn { get; init; }
  public int PageNumber { get; init; }
  public int PageSize { get; init; }
}