using MediatR;
using TravelBookingPlatform.Application.Owners.Common;
using TravelBookingPlatform.Domain.Enums;
using TravelBookingPlatform.Domain.Model;

namespace TravelBookingPlatform.Application.Owners.Get;

public class GetOwnersQuery : IRequest<PaginatedResult<OwnerResponse>>
{
  public string? SearchTerm { get; init; }
  public SortOrder? SortOrder { get; init; }
  public string? SortColumn { get; init; }
  public int PageNumber { get; init; }
  public int PageSize { get; init; }
}