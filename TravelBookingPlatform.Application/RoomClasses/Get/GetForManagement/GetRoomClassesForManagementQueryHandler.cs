using System.Linq.Expressions;
using AutoMapper;
using MediatR;
using TravelBookingPlatform.Domain.Entities;
using TravelBookingPlatform.Domain.Enums;
using TravelBookingPlatform.Domain.Interfaces.Persistence.Repositories;
using TravelBookingPlatform.Domain.Model;

namespace TravelBookingPlatform.Application.RoomClasses.GetForManagement;

public class GetRoomClassesForManagementQueryHandler : IRequestHandler<GetRoomClassesForManagementQuery,
  PaginatedResult<RoomClassForManagementResponse>>
{
  private readonly IMapper _mapper;
  private readonly IRoomClassRepository _roomClassRepository;

  public GetRoomClassesForManagementQueryHandler(IRoomClassRepository roomClassRepository, IMapper mapper)
  {
    _roomClassRepository = roomClassRepository;
    _mapper = mapper;
  }

  public async Task<PaginatedResult<RoomClassForManagementResponse>> Handle(GetRoomClassesForManagementQuery request,
    CancellationToken cancellationToken)
  {
    var query = new EntityQuery<RoomCategory>(
      GetSearchExpression(request.SearchTerm),
      request.SortOrder ?? SortOrder.Ascending,
      request.SortColumn,
      request.PageNumber,
      request.PageSize);

    var owners = await _roomClassRepository.GetAsync(
      query,
      false,
      cancellationToken);

    return _mapper.Map<PaginatedResult<RoomClassForManagementResponse>>(owners);
  }

  private static Expression<Func<RoomCategory, bool>> GetSearchExpression(string? searchTerm)
  {
    return searchTerm is null
      ? _ => true
      : rc => rc.Name.Contains(searchTerm);
  }
}