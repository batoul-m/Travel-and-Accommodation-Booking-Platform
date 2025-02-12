using MediatR;
using TravelBookingPlatform.Domain.Exceptions;
using TravelBookingPlatform.Domain.Interfaces.Persistence;
using TravelBookingPlatform.Domain.Interfaces.Persistence.Repositories;
using TravelBookingPlatform.Domain.Messages;

namespace TravelBookingPlatform.Application.Hotels.Delete;

public class DeleteHotelCommandHandler : IRequestHandler<DeleteHotelCommand>
{
  private readonly IHotelRepository _hotelRepository;
  private readonly IRoomClassRepository _roomClassRepository;
  private readonly IUnitOfWork _unitOfWork;

  public DeleteHotelCommandHandler(
    IHotelRepository hotelRepository,
    IUnitOfWork unitOfWork,
    IRoomClassRepository roomClassRepository)
  {
    _hotelRepository = hotelRepository;
    _unitOfWork = unitOfWork;
    _roomClassRepository = roomClassRepository;
  }

  public async Task Handle(DeleteHotelCommand request, CancellationToken cancellationToken = default)
  {
    if (!await _hotelRepository.ExistsAsync(h => h.Id == request.HotelId, cancellationToken))
    {
      throw new NotFoundException(HotelMessages.NotFoundById);
    }

    if (await _roomClassRepository.ExistsAsync(rc => rc.HotelId == request.HotelId, cancellationToken))
    {
      throw new DependentsExistException(HotelMessages.CannotDeleteWithDependencies);
    }

    await _hotelRepository.DeleteAsync(request.HotelId, cancellationToken);

    await _unitOfWork.SaveChangesAsync(cancellationToken);
  }
}