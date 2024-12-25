using AutoMapper;
using MediatR;
using TravelBookingPlatform.Domain.Exceptions;
using TravelBookingPlatform.Domain.Interfaces.Persistence;
using TravelBookingPlatform.Domain.Interfaces.Persistence.Repositories;
using TravelBookingPlatform.Domain.Messages;

namespace TravelBookingPlatform.Application.Amenities;

public class UpdateAmenityCommandHandler : IRequestHandler<UpdateAmenity>
{
  private readonly IAmenityRepository _amenityRepository;
  private readonly IMapper _mapper;
  private readonly IUnitOfWork _unitOfWork;

  public UpdateAmenityCommandHandler(
    IAmenityRepository amenityRepository,
    IUnitOfWork unitOfWork,
    IMapper mapper)
  {
    _amenityRepository = amenityRepository;
    _unitOfWork = unitOfWork;
    _mapper = mapper;
  }

  public async Task Handle(
    UpdateAmenity request,
    CancellationToken cancellationToken = default)
  {
    var amenityEntity = await _amenityRepository.GetByIdAsync(
                          request.AmenityId,
                          cancellationToken) ??
                        throw new NotFoundException(AmenityMessages.NotFoundById);

    if (!await _amenityRepository.ExistsAsync(
          a => a.Name == request.Name,
          cancellationToken))
    {
      throw new AmenityExistsException(AmenityMessages.DuplicateName);
    }

    _mapper.Map(request, amenityEntity);

    await _amenityRepository.UpdateAsync(
      amenityEntity,
      cancellationToken);

    await _unitOfWork.SaveChangesAsync(cancellationToken);
  }
}