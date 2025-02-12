using System.Linq.Expressions;
using AutoMapper;
using MediatR;
using TravelBookingPlatform.Application.Owners.Common;
using TravelBookingPlatform.Domain.Entities;
using TravelBookingPlatform.Domain.Enums;
using TravelBookingPlatform.Domain.Interfaces.Persistence.Repositories;
using TravelBookingPlatform.Domain.Model;

namespace TravelBookingPlatform.Application.Owners.Get;

public class GetOwnersQueryHandler : IRequestHandler<GetOwnersQuery, PaginatedResult<OwnerResponse>>
{
  private readonly IMapper _mapper;
  private readonly IOwnerRepository _ownerRepository;

  public GetOwnersQueryHandler(
    IOwnerRepository ownerRepository,
    IMapper mapper)
  {
    _ownerRepository = ownerRepository;
    _mapper = mapper;
  }

  public async Task<PaginatedResult<OwnerResponse>> Handle(
    GetOwnersQuery request,
    CancellationToken cancellationToken)
  {
    var query = new EntityQuery<Owner>(
      GetSearchExpression(request.SearchTerm),
      request.SortOrder ?? SortOrder.Ascending,
      request.SortColumn,
      request.PageNumber,
      request.PageSize);

    var owners = await _ownerRepository.GetAsync(query, cancellationToken);

    return _mapper.Map<PaginatedResult<OwnerResponse>>(owners);
  }

  private static Expression<Func<Owner, bool>> GetSearchExpression(string? searchTerm)
  {
    return searchTerm is null
      ? _ => true
      : o => o.FirstName.Contains(searchTerm) || o.LastName.Contains(searchTerm);
  }
}