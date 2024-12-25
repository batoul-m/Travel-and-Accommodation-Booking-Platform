using AutoMapper;
using MediatR;
using TravelBookingPlatform.Domain.Exceptions;
using TravelBookingPlatform.Domain.Interfaces.Persistence;
using TravelBookingPlatform.Domain.Interfaces.Persistence.Repositories;
using TravelBookingPlatform.Domain.Messages;

namespace TravelBookingPlatform.Application.Owners.Update;

public class UpdateOwnerCommandHandler : IRequestHandler<UpdateOwnerCommand>
{
  private readonly IMapper _mapper;
  private readonly IOwnerRepository _ownerRepository;
  private readonly IUnitOfWork _unitOfWork;

  public UpdateOwnerCommandHandler(
    IOwnerRepository ownerRepository,
    IUnitOfWork unitOfWork,
    IMapper mapper)
  {
    _ownerRepository = ownerRepository;
    _unitOfWork = unitOfWork;
    _mapper = mapper;
  }

  public async Task Handle(UpdateOwnerCommand request, CancellationToken cancellationToken = default)
  {
    var ownerEntity = await _ownerRepository.GetByIdAsync(request.OwnerId, cancellationToken)
                      ?? throw new NotFoundException(OwnerMessages.NotFoundById);

    _mapper.Map(request, ownerEntity);

    await _ownerRepository.UpdateAsync(ownerEntity, cancellationToken);

    await _unitOfWork.SaveChangesAsync(cancellationToken);
  }
}