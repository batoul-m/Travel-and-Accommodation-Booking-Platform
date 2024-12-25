using AutoMapper;
using MediatR;
using TravelBookingPlatform.Domain.Exceptions;
using TravelBookingPlatform.Domain.Interfaces.Persistence;
using TravelBookingPlatform.Domain.Interfaces.Persistence.Repositories;
using TravelBookingPlatform.Domain.Messages;

namespace TravelBookingPlatform.Application.Rooms.Update;

public class UpdateRoomCommandHandler : IRequestHandler<UpdateRoomCommand>
{
  private readonly IMapper _mapper;
  private readonly IRoomClassRepository _roomClassRepository;
  private readonly IRoomRepository _roomRepository;
  private readonly IUnitOfWork _unitOfWork;

  public UpdateRoomCommandHandler(
    IRoomRepository roomRepository,
    IRoomClassRepository roomClassRepository,
    IMapper mapper,
    IUnitOfWork unitOfWork)
  {
    _roomRepository = roomRepository;
    _roomClassRepository = roomClassRepository;
    _mapper = mapper;
    _unitOfWork = unitOfWork;
  }

  public async Task Handle(UpdateRoomCommand request, CancellationToken cancellationToken)
  {
    if (!await _roomClassRepository.ExistsAsync(
          rc => rc.Id == request.RoomClassId, 
          cancellationToken))
    {
      throw new NotFoundException(RoomCategoryMessages.NotFoundById);
    }

    if (await _roomRepository.ExistsAsync(
          r => r.RoomCategoryId == request.RoomClassId &&
               r.Number == request.Number, 
          cancellationToken))
    {
      throw new DuplicateRoomNumberException(RoomCategoryMessages.DuplicateRoomNumber);
    }

    var roomEntity = await _roomRepository.GetByIdAsync(
      request.RoomClassId, request.RoomId,
      cancellationToken);

    if (roomEntity is null)
    {
      throw new NotFoundException(RoomCategoryMessages.RoomDoesNotExist);
    }

    _mapper.Map(request, roomEntity);

    await _roomRepository.UpdateAsync(roomEntity, cancellationToken);

    await _unitOfWork.SaveChangesAsync(cancellationToken);
  }
}