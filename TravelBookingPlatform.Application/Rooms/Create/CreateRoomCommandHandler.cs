using AutoMapper;
using MediatR;
using TravelBookingPlatform.Domain.Entities;
using TravelBookingPlatform.Domain.Exceptions;
using TravelBookingPlatform.Domain.Interfaces.Persistence;
using TravelBookingPlatform.Domain.Interfaces.Persistence.Repositories;
using TravelBookingPlatform.Domain.Messages;

namespace TravelBookingPlatform.Application.Rooms.Create;

public class CreateRoomHandler : IRequestHandler<CreateRoomCommand, Guid>
{
  private readonly IMapper _mapper;
  private readonly IRoomClassRepository _roomClassRepository;
  private readonly IRoomRepository _roomRepository;
  private readonly IUnitOfWork _unitOfWork;

  public CreateRoomHandler(
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

  public async Task<Guid> Handle(CreateRoomCommand request, CancellationToken cancellationToken)
  {
    if (!await _roomClassRepository.ExistsAsync(rc => rc.Id == request.RoomClassId, cancellationToken))
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

    var createdRoom = await _roomRepository.CreateAsync(
      _mapper.Map<Room>(request),
      cancellationToken);

    await _unitOfWork.SaveChangesAsync(cancellationToken);

    return createdRoom.RoomId;
  }
}