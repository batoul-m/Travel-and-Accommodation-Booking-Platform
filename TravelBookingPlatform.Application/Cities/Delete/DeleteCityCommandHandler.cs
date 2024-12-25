using MediatR;
using TravelBookingPlatform.Domain.Exceptions;
using TravelBookingPlatform.Domain.Interfaces.Persistence;
using TravelBookingPlatform.Domain.Interfaces.Persistence.Repositories;
using TravelBookingPlatform.Domain.Messages;

namespace TravelBookingPlatform.Application.Cities.Delete;

public class DeleteCityCommandHandler : IRequestHandler<DeleteCityCommand>
{
  private readonly ICityRepository _cityRepository;
  private readonly IHotelRepository _hotelRepository;
  private readonly IUnitOfWork _unitOfWork;

  public DeleteCityCommandHandler(
    ICityRepository cityRepository,
    IUnitOfWork unitOfWork,
    IHotelRepository hotelRepository)
  {
    _cityRepository = cityRepository;
    _unitOfWork = unitOfWork;
    _hotelRepository = hotelRepository;
  }

  public async Task Handle(DeleteCityCommand request, CancellationToken cancellationToken)
  {
    if (!await _cityRepository.ExistsAsync(c => c.Id == request.CityId, cancellationToken))
    {
      throw new NotFoundException(CityMessages.NotFoundById);
    }

    if (await _hotelRepository.ExistsAsync(h => h.CityId == request.CityId, cancellationToken))
    {
      throw new DependentsExistException(CityMessages.CannotDeleteWithDependencies);
    }

    await _cityRepository.DeleteAsync(request.CityId, cancellationToken);

    await _unitOfWork.SaveChangesAsync(cancellationToken);
  }
}