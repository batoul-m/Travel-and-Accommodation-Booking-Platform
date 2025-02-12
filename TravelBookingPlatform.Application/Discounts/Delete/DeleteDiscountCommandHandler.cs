using MediatR;
using TravelBookingPlatform.Domain.Exceptions;
using TravelBookingPlatform.Domain.Interfaces.Persistence;
using TravelBookingPlatform.Domain.Interfaces.Persistence.Repositories;
using TravelBookingPlatform.Domain.Messages;

namespace TravelBookingPlatform.Application.Discounts.Delete;

public class DeleteDiscountCommandHandler : IRequestHandler<DeleteDiscountCommand>
{
  private readonly IDiscountRepository _discountRepository;
  private readonly IRoomClassRepository _roomClassRepository;
  private readonly IUnitOfWork _unitOfWork;

  public DeleteDiscountCommandHandler(
    IRoomClassRepository roomClassRepository,
    IDiscountRepository discountRepository,
    IUnitOfWork unitOfWork)
  {
    _roomClassRepository = roomClassRepository;
    _discountRepository = discountRepository;
    _unitOfWork = unitOfWork;
  }

  public async Task Handle(DeleteDiscountCommand request, CancellationToken cancellationToken)
  {
    if (!await _roomClassRepository.ExistsAsync(
          rc => rc.Id == request.RoomCategoryId,
          cancellationToken))
    {
      throw new NotFoundException(RoomCategoryMessages.RoomDoesNotExist);
    }

    if (!await _discountRepository.ExistsAsync(
          d => d.Id == request.DiscountId && d.RoomClassId == request.RoomCategoryId, 
          cancellationToken))
    {
      throw new NotFoundException(DiscountMessages.NotFoundInRoomClass);
    }

    await _discountRepository.DeleteAsync(request.DiscountId, cancellationToken);

    await _unitOfWork.SaveChangesAsync(cancellationToken);
  }
}