using System.Linq.Expressions;
using AutoMapper;
using MediatR;
using TravelBookingPlatform.Domain.Entities;
using TravelBookingPlatform.Domain.Enums;
using TravelBookingPlatform.Domain.Interfaces.Persistence.Repositories;
using TravelBookingPlatform.Domain.Model;

namespace TravelBookingPlatform.Application.Cities.GetForManagement;

public class
  GetCitiesForManagementHandler : IRequestHandler<GetCitiesForManagementQuery, PaginatedResult<CityForManagementResponse>>
{
  private readonly ICityRepository _cityRepository;
  private readonly IMapper _mapper;

  public GetCitiesForManagementHandler(
    ICityRepository cityRepository,
    IMapper mapper)
  {
    _cityRepository = cityRepository;
    _mapper = mapper;
  }

  public async Task<PaginatedResult<CityForManagementResponse>> Handle(
    GetCitiesForManagementQuery request,
    CancellationToken cancellationToken)
  {
    var query = new EntityQuery<City>(
      GetSearchExpression(request.SearchTerm),
      request.SortOrder ?? SortOrder.Ascending,
      request.SortColumn,
      request.PageNumber,
      request.PageSize);

    var owners = await _cityRepository.GetForManagementAsync(query, cancellationToken);

    return _mapper.Map<PaginatedResult<CityForManagementResponse>>(owners);
  }

  private static Expression<Func<City, bool>> GetSearchExpression(string? searchTerm)
  {
    return searchTerm is null
      ? _ => true
      : o => o.Name.Contains(searchTerm) || o.Country.Contains(searchTerm);
  }
}