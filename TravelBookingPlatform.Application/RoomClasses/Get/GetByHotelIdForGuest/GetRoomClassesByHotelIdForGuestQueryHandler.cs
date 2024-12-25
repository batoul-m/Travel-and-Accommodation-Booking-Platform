using AutoMapper;
using MediatR;
using TravelBookingPlatform.Domain.Entities;
using TravelBookingPlatform.Domain.Enums;
using TravelBookingPlatform.Domain.Exceptions;
using TravelBookingPlatform.Domain.Interfaces.Persistence.Repositories;
using TravelBookingPlatform.Domain.Messages;
using TravelBookingPlatform.Domain.Model;

namespace TravelBookingPlatform.Application.RoomClasses.GetByHotelIdForGuest;

public class GetRoomClassesByHotelIdForGuestQueryHandler : IRequestHandler<GetRoomClassesByHotelIdForGuestQuery,
  PaginatedResult<RoomClassForGuestResponse>>
{
  private readonly IHotelRepository _hotelRepository;
  private readonly IMapper _mapper;
  private readonly IRoomClassRepository _roomClassRepository;

  public GetRoomClassesByHotelIdForGuestQueryHandler(IHotelRepository hotelRepository,
    IRoomClassRepository roomClassRepository, IMapper mapper)
  {
    _hotelRepository = hotelRepository;
    _roomClassRepository = roomClassRepository;
    _mapper = mapper;
  }

  public async Task<PaginatedResult<RoomClassForGuestResponse>> Handle(GetRoomClassesByHotelIdForGuestQuery request,
    CancellationToken cancellationToken = default)
  {
    if (!await _hotelRepository.ExistsAsync(h => h.Id == request.HotelId, cancellationToken))
    {
      throw new NotFoundException(HotelMessages.NotFoundById);
    }

    var roomClasses = await _roomClassRepository.GetAsync(
      new EntityQuery<RoomCategory>(
        rc => rc.HotelId == request.HotelId,
        request.SortOrder ?? SortOrder.Ascending,
        request.SortColumn,
        request.PageNumber,
        request.PageSize),
      includeGallery: true,
      cancellationToken);

    return _mapper.Map<PaginatedResult<RoomClassForGuestResponse>>(roomClasses);
  }
}