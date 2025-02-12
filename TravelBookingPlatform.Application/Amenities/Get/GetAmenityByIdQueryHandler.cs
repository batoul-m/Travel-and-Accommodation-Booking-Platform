using AutoMapper;
using MediatR;
using TravelBookingPlatform.Domain.Exceptions;
using TravelBookingPlatform.Domain.Interfaces.Persistence.Repositories;
using TravelBookingPlatform.Domain.Messages;

namespace TravelBookingPlatform.Application.Amenities;

public class GetAmenityByIdQueryHandler : IRequestHandler<GetAmenityByIdQuery, AmenityResponse>
{
  private readonly IAmenityRepository _amenityRepository;
  private readonly IMapper _mapper;

  public GetAmenityByIdQueryHandler(IAmenityRepository amenityRepository, IMapper mapper)
  {
    _amenityRepository = amenityRepository;
    _mapper = mapper;
  }

  public async Task<AmenityResponse> Handle(
    GetAmenityByIdQuery request,
    CancellationToken cancellationToken = default)
  {
    var amenity = await _amenityRepository.GetByIdAsync(
                    request.AmenityId,
                    cancellationToken) ??
                  throw new NotFoundException(AmenityMessages.NotFoundById);

    return _mapper.Map<AmenityResponse>(amenity);
  }
}