using AutoMapper;
using MediatR;
using TravelBookingPlatform.Application.Owners.Common;
using TravelBookingPlatform.Domain.Exceptions;
using TravelBookingPlatform.Domain.Interfaces.Persistence.Repositories;
using TravelBookingPlatform.Domain.Messages;

namespace TravelBookingPlatform.Application.Owners.GetById;

public class GetOwnerByIdQueryHandler : IRequestHandler<GetOwnerByIdQuery, OwnerResponse>
{
  private readonly IMapper _mapper;
  private readonly IOwnerRepository _ownerRepository;

  public GetOwnerByIdQueryHandler(IOwnerRepository ownerRepository, IMapper mapper)
  {
    _ownerRepository = ownerRepository;
    _mapper = mapper;
  }

  public async Task<OwnerResponse> Handle(
    GetOwnerByIdQuery request,
    CancellationToken cancellationToken)
  {
    var owner = await _ownerRepository.GetByIdAsync(request.OwnerId, cancellationToken)
                ?? throw new NotFoundException(OwnerMessages.NotFoundById);

    return _mapper.Map<OwnerResponse>(owner);
  }
}