using AutoMapper;
using MediatR;
using TravelBookingPlatform.Application.Amenities;
using TravelBookingPlatform.Domain.Entities;
using TravelBookingPlatform.Domain.Exceptions;
using TravelBookingPlatform.Domain.Interfaces.Persistence;
using TravelBookingPlatform.Domain.Interfaces.Persistence.Repositories;
using TravelBookingPlatform.Domain.Messages;

namespace TravelBookingPlatform.Application.Amenities.Create;
public class CreateAmenityHandler : IRequestHandler<CreateAmenity, AmenityResponse>
{
  private readonly IAmenityRepository _amenityRepository;
  private readonly IMapper _mapper;
  private readonly IUnitOfWork _unitOfWork;

  public CreateAmenityHandler(
    IAmenityRepository amenityRepository,
    IUnitOfWork unitOfWork,
    IMapper mapper)
  {
    _amenityRepository = amenityRepository;
    _unitOfWork = unitOfWork;
    _mapper = mapper;
  }

  public async Task<AmenityResponse> Handle(
    CreateAmenity request,
    CancellationToken cancellationToken = default)
  {
    if (await _amenityRepository.ExistsAsync(a => a.Name == request.Name, cancellationToken))
    {
      throw new AmenityExistsException(AmenityMessages.DuplicateName);
    }

    var createdAmenity = await _amenityRepository.CreateAsync(
      _mapper.Map<Amenity>(request),
      cancellationToken);

    await _unitOfWork.SaveChangesAsync(cancellationToken);

    return _mapper.Map<AmenityResponse>(createdAmenity);
  }
}