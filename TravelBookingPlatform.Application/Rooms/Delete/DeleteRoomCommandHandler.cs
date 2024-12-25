using MediatR;
using TravelBookingPlatform.Domain.Exceptions;
using TravelBookingPlatform.Domain.Interfaces.Persistence;
using TravelBookingPlatform.Domain.Interfaces.Persistence.Repositories;
using TravelBookingPlatform.Domain.Messages;

namespace TravelBookingPlatform.Application.Rooms.Delete;

public class DeleteRoomCommandHandler : IRequestHandler<DeleteRoomCommand>
{
  private readonly IBookingRepository _bookingRepository;
  private readonly IRoomClassRepository _roomClassRepository;
  private readonly IRoomRepository _roomRepository;
  private readonly IUnitOfWork _unitOfWork;

  public DeleteRoomCommandHandler(
    IRoomRepository roomRepository,
    IUnitOfWork unitOfWork,
    IRoomClassRepository roomClassRepository,
    IBookingRepository bookingRepository)
  {
    _roomRepository = roomRepository;
    _unitOfWork = unitOfWork;
    _roomClassRepository = roomClassRepository;
    _bookingRepository = bookingRepository;
  }

  public async Task Handle(DeleteRoomCommand request, CancellationToken cancellationToken)
  {
    if (!await _roomClassRepository.ExistsAsync(
          rc => rc.Id == request.RoomClassId, 
          cancellationToken))
    {
      throw new NotFoundException(RoomCategoryMessages.NotFoundById);
    }

    if (!await _roomRepository.ExistsAsync(
          r => r.RoomId == request.RoomId && r.RoomCategoryId == request.RoomClassId, 
          cancellationToken))
    {
      throw new NotFoundException(RoomCategoryMessages.RoomDoesNotExist);
    }

    if (await _bookingRepository.ExistsAsync(b => b.Rooms.Any(r => r.RoomId == request.RoomId), cancellationToken))
    {
      throw new DependentsExistException(RoomMessages.CannotDeleteWithBookings);
    }

    await _roomRepository.DeleteAsync(request.RoomId, cancellationToken);

    await _unitOfWork.SaveChangesAsync(cancellationToken);
  }
}