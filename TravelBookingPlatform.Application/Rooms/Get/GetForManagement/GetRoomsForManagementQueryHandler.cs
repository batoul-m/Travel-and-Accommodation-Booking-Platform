using System.Linq.Expressions;
using AutoMapper;
using MediatR;
using TravelBookingPlatform.Application.Extensions;
using TravelBookingPlatform.Domain.Entities;
using TravelBookingPlatform.Domain.Enums;
using TravelBookingPlatform.Domain.Exceptions;
using TravelBookingPlatform.Domain.Interfaces.Persistence.Repositories;
using TravelBookingPlatform.Domain.Messages;
using TravelBookingPlatform.Domain.Model;

namespace TravelBookingPlatform.Application.Rooms.GetForManagement;

public class GetRoomsHandler : IRequestHandler<GetRoomsForManagementQuery, PaginatedResult<RoomForManagementResponse>>
{
  private readonly IMapper _mapper;
  private readonly IRoomClassRepository _roomClassRepository;
  private readonly IRoomRepository _roomRepository;

  public GetRoomsHandler(
    IRoomRepository roomRepository,
    IMapper mapper,
    IRoomClassRepository roomClassRepository)
  {
    _roomRepository = roomRepository;
    _mapper = mapper;
    _roomClassRepository = roomClassRepository;
  }

  public async Task<PaginatedResult<RoomForManagementResponse>> Handle(GetRoomsForManagementQuery request,
    CancellationToken cancellationToken)
  {
    if (!await _roomClassRepository.ExistsAsync(
          rc => rc.Id == request.RoomClassId,
          cancellationToken))
    {
      throw new NotFoundException(RoomCategoryMessages.NotFoundById);
    }

    var query = new EntityQuery<Room>(
      GetSearchExpression(request.SearchTerm)
        .And(r => r.RoomCategoryId == request.RoomClassId),
      request.SortOrder ?? SortOrder.Ascending,
      request.SortColumn,
      request.PageNumber,
      request.PageSize);

    var owners = await _roomRepository.GetForManagementAsync(query, cancellationToken);

    return _mapper.Map<PaginatedResult<RoomForManagementResponse>>(owners);
  }

  private static Expression<Func<Room, bool>> GetSearchExpression(string? searchTerm)
  {
    return searchTerm is null
      ? _ => true
      : r => r.Number.Contains(searchTerm);
  }
}