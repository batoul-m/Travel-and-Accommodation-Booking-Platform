using AutoMapper;
using MediatR;
using TravelBookingPlatform.Domain.Exceptions;
using TravelBookingPlatform.Domain.Interfaces.Persistence;
using TravelBookingPlatform.Domain.Interfaces.Persistence.Repositories;
using TravelBookingPlatform.Domain.Messages;

namespace TravelBookingPlatform.Application.Cities.Update;

public class UpdateCityCommandHandler : IRequestHandler<UpdateCityCommand>
{
  private readonly ICityRepository _cityRepository;
  private readonly IMapper _mapper;
  private readonly IUnitOfWork _unitOfWork;

  public UpdateCityCommandHandler(ICityRepository cityRepository, IUnitOfWork unitOfWork, IMapper mapper)
  {
    _cityRepository = cityRepository;
    _unitOfWork = unitOfWork;
    _mapper = mapper;
  }

  public async Task Handle(UpdateCityCommand request, CancellationToken cancellationToken)
  {
    if (await _cityRepository.ExistsAsync(c => c.PostOffice == request.PostOffice, cancellationToken))
    {
      throw new DuplicatePostOfficeException(CityMessages.DuplicatePostalCode);
    }

    var cityEntity = await _cityRepository.GetByIdAsync(request.CityId, cancellationToken) ??
                     throw new NotFoundException(CityMessages.NotFoundById);

    _mapper.Map(request, cityEntity);

    await _cityRepository.UpdateAsync(cityEntity, cancellationToken);

    await _unitOfWork.SaveChangesAsync(cancellationToken);
  }
}