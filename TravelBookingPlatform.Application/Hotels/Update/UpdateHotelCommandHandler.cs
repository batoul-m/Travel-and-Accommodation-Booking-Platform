using AutoMapper;
using MediatR;
using TravelBookingPlatform.Domain.Exceptions;
using TravelBookingPlatform.Domain.Interfaces.Persistence;
using TravelBookingPlatform.Domain.Interfaces.Persistence.Repositories;
using TravelBookingPlatform.Domain.Messages;

namespace TravelBookingPlatform.Application.Hotels.Update;

public class UpdateHotelCommandHandler : IRequestHandler<UpdateHotelCommand>
{
  private readonly ICityRepository _cityRepository;
  private readonly IHotelRepository _hotelRepository;
  private readonly IMapper _mapper;
  private readonly IOwnerRepository _ownerRepository;
  private readonly IUnitOfWork _unitOfWork;

  public UpdateHotelCommandHandler(
    ICityRepository cityRepository,
    IOwnerRepository ownerRepository,
    IHotelRepository hotelRepository,
    IUnitOfWork unitOfWork,
    IMapper mapper)
  {
    _cityRepository = cityRepository;
    _ownerRepository = ownerRepository;
    _hotelRepository = hotelRepository;
    _unitOfWork = unitOfWork;
    _mapper = mapper;
  }

  public async Task Handle(UpdateHotelCommand request, CancellationToken cancellationToken = default)
  {
    var hotelEntity = await _hotelRepository.GetByIdAsync(
                        request.HotelId,
                        false,
                        false,
                        false,
                        cancellationToken)
                      ?? throw new NotFoundException(HotelMessages.NotFoundById);

    if (!await _cityRepository.ExistsAsync(c => c.Id == request.CityId, cancellationToken))
    {
      throw new NotFoundException(CityMessages.NotFoundById);
    }

    if (!await _ownerRepository.ExistsAsync(o => o.Id == request.OwnerId, cancellationToken))
    {
      throw new NotFoundException(OwnerMessages.NotFoundById);
    }

    if (await _hotelRepository.ExistsAsync(
          h => h.Longitude == request.Longitude && h.Longitude == request.Latitude,
          cancellationToken))
    {
      throw new DuplicateHotelLocationException(HotelMessages.DuplicateLocation);
    }

    _mapper.Map(request, hotelEntity);

    await _hotelRepository.UpdateAsync(hotelEntity, cancellationToken);

    await _unitOfWork.SaveChangesAsync(cancellationToken);
  }
}