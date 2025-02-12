using AutoMapper;
using MediatR;
using TravelBookingPlatform.Domain.Entities;
using TravelBookingPlatform.Domain.Enums;
using TravelBookingPlatform.Domain.Exceptions;
using TravelBookingPlatform.Domain.Interfaces.Persistence.Repositories;
using TravelBookingPlatform.Domain.Messages;
using TravelBookingPlatform.Domain.Model;

namespace TravelBookingPlatform.Application.Rooms.GetByRoomClassIdForGuest;

public class GetRoomsByRoomClassIdForGuestsQueryHandler :
  IRequestHandler<GetRoomsByRoomClassIdForGuestsQuery,
    PaginatedResult<RoomForGuestResponse>>
{
  private readonly IMapper _mapper;
  private readonly IRoomClassRepository _roomClassRepository;
  private readonly IRoomRepository _roomRepository;

  public GetRoomsByRoomClassIdForGuestsQueryHandler(IRoomClassRepository roomClassRepository,
    IRoomRepository roomRepository, IMapper mapper)
  {
    _roomClassRepository = roomClassRepository;
    _roomRepository = roomRepository;
    _mapper = mapper;
  }

  public async Task<PaginatedResult<RoomForGuestResponse>> Handle(
    GetRoomsByRoomClassIdForGuestsQuery request,
    CancellationToken cancellationToken = default)
  {
    if (!await _roomClassRepository.ExistsAsync(
          rc => rc.Id == request.RoomClassId, 
          cancellationToken))
    {
      throw new NotFoundException(RoomCategoryMessages.NotFoundById);
    }

    var query = new EntityQuery<Room>(
      r => r.RoomCategoryId == request.RoomClassId &&
           !r.Bookings.Any(b => request.CheckInDate >= b.CheckOutDateUtc
                                || request.CheckOutDate <= b.CheckInDateUtc),
      SortOrder.Ascending,
      null,
      request.PageNumber,
      request.PageSize);

    var rooms = await _roomRepository.GetAsync(query, cancellationToken);

    return _mapper.Map<PaginatedResult<RoomForGuestResponse>>(rooms);
  }
}