using System.Data;
using AutoMapper;
using MediatR;
using TravelBookingPlatform.Domain.Exceptions;
using TravelBookingPlatform.Domain.Interfaces.Persistence;
using TravelBookingPlatform.Domain.Interfaces.Persistence.Repositories;
using TravelBookingPlatform.Domain.Messages;

namespace TravelBookingPlatform.Application.RoomClasses.Update;

public class UpdateRoomClassHandler : IRequestHandler<UpdateRoomClassCommand>
{
  private readonly IMapper _mapper;
  private readonly IRoomClassRepository _roomClassRepository;
  private readonly IUnitOfWork _unitOfWork;

  public UpdateRoomClassHandler(
    IRoomClassRepository roomClassRepository,
    IUnitOfWork unitOfWork,
    IMapper mapper)
  {
    _roomClassRepository = roomClassRepository;
    _unitOfWork = unitOfWork;
    _mapper = mapper;
  }

  public async Task Handle(
    UpdateRoomClassCommand request,
    CancellationToken cancellationToken)
  {
    var roomClassEntity = await _roomClassRepository.GetByIdAsync(request.RoomClassId, cancellationToken);

    if (roomClassEntity is null)
    {
      throw new NotFoundException(RoomCategoryMessages.NotFoundById);
    }

    if (await _roomClassRepository.ExistsAsync(
          rc => rc.HotelId == roomClassEntity.HotelId &&
                rc.Name == request.Name,
          cancellationToken))
    {
      throw new DuplicateNameException(RoomCategoryMessages.DuplicateNameInHotel);
    }

    _mapper.Map(request, roomClassEntity);

    await _roomClassRepository.UpdateAsync(roomClassEntity, cancellationToken);

    await _unitOfWork.SaveChangesAsync(cancellationToken);
  }
}