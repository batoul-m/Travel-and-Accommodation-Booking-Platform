using System.Linq.Expressions;
using AutoMapper;
using MediatR;
using TravelBookingPlatform.Domain.Entities;
using TravelBookingPlatform.Domain.Enums;
using TravelBookingPlatform.Domain.Interfaces.Persistence.Repositories;
using TravelBookingPlatform.Domain.Model;

namespace TravelBookingPlatform.Application.Hotels.GetForManagement;

public class GetHotelsForManagementQueryHandler : IRequestHandler<GetHotelsForManagementQuery,
  PaginatedResult<HotelForManagementResponse>>
{
  private readonly IHotelRepository _hotelRepository;
  private readonly IMapper _mapper;

  public GetHotelsForManagementQueryHandler(IHotelRepository hotelRepository, IMapper mapper)
  {
    _hotelRepository = hotelRepository;
    _mapper = mapper;
  }

  public async Task<PaginatedResult<HotelForManagementResponse>> Handle(
    GetHotelsForManagementQuery request,
    CancellationToken cancellationToken)
  {
    var query = new EntityQuery<Hotel>(
      GetSearchExpression(request.SearchTerm),
      request.SortOrder ?? SortOrder.Ascending,
      request.SortColumn,
      request.PageNumber,
      request.PageSize);

    var owners = await _hotelRepository.GetForManagementAsync(query, cancellationToken);

    return _mapper.Map<PaginatedResult<HotelForManagementResponse>>(owners);
  }

  private static Expression<Func<Hotel, bool>> GetSearchExpression(string? searchTerm)
  {
    return searchTerm is null
      ? _ => true
      : h => h.Name.Contains(searchTerm) || h.City.Name.Contains(searchTerm);
  }
}