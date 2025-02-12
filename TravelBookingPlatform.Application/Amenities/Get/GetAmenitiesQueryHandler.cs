using System.Linq.Expressions;
using AutoMapper;
using MediatR;
using TravelBookingPlatform.Application.Amenities;
using TravelBookingPlatform.Domain.Entities;
using TravelBookingPlatform.Domain.Enums;
using TravelBookingPlatform.Domain.Interfaces.Persistence.Repositories;
using TravelBookingPlatform.Domain.Model;

namespace TravelBookingPlatform.Application.Amenities;

public class GetAmenitiesQueryHandler : IRequestHandler<GetAmenitiesQuery, PaginatedResult<AmenityResponse>>
{
  private readonly IAmenityRepository _amenityRepository;
  private readonly IMapper _mapper;

  public GetAmenitiesQueryHandler(
    IAmenityRepository amenityRepository,
    IMapper mapper)
  {
    _amenityRepository = amenityRepository;
    _mapper = mapper;
  }

  public async Task<PaginatedResult<AmenityResponse>> Handle(
    GetAmenitiesQuery request,
    CancellationToken cancellationToken = default)
  {
    var query = new EntityQuery<Amenity>(
      GetSearchExpression(request.SearchTerm),
      request.SortOrder ?? SortOrder.Ascending,
      request.SortColumn,
      request.PageNumber,
      request.PageSize);

    var owners = await _amenityRepository.GetAsync(query, cancellationToken);

    return _mapper.Map<PaginatedResult<AmenityResponse>>(owners);
  }

  private static Expression<Func<Amenity, bool>> GetSearchExpression(string? searchTerm)
  {
    return string.IsNullOrEmpty(searchTerm)
      ? _ => true
      : o => o.Name.Contains(searchTerm);
  }
}