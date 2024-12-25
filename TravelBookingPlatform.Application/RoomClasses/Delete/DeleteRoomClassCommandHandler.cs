using MediatR;
using TravelBookingPlatform.Domain;
using TravelBookingPlatform.Domain.Exceptions;
using TravelBookingPlatform.Domain.Interfaces.Persistence;
using TravelBookingPlatform.Domain.Interfaces.Persistence.Repositories;
using TravelBookingPlatform.Domain.Messages;

namespace TravelBookingPlatform.Application.RoomClasses.Delete;

public class DeleteRoomClassCommandHandler : IRequestHandler<DeleteRoomClassCommand>
{
  private readonly IRoomClassRepository _roomClassRepository;
  private readonly IRoomRepository _roomRepository;
  private readonly IUnitOfWork _unitOfWork;

  public DeleteRoomClassCommandHandler(IRoomClassRepository roomClassRepository, IUnitOfWork unitOfWork,
    IRoomRepository roomRepository)
  {
    _roomClassRepository = roomClassRepository;
    _unitOfWork = unitOfWork;
    _roomRepository = roomRepository;
  }
  
  public async Task Handle(DeleteRoomClassCommand request, CancellationToken cancellationToken)
  {
    if (!await _roomClassRepository.ExistsAsync(rc => rc.Id == request.RoomClassId, cancellationToken))
    {
      throw new NotFoundException(RoomCategoryMessages.RoomDoesNotExist);
    }

    if (await _roomRepository.ExistsAsync(r => r.RoomCategoryId == request.RoomClassId, cancellationToken))
    {
      throw new DependentsExistException(RoomCategoryMessages.CannotDeleteWithDependencies);
    }

    await _roomClassRepository.DeleteAsync(request.RoomClassId, cancellationToken);

    await _unitOfWork.SaveChangesAsync(cancellationToken);
  }
}