using MediatR;
using TravelBookingPlatform.Domain;
using TravelBookingPlatform.Domain.Enums;
using TravelBookingPlatform.Domain.Exceptions;
using TravelBookingPlatform.Domain.Interfaces.Persistence;
using TravelBookingPlatform.Domain.Interfaces.Persistence.Repositories;
using TravelBookingPlatform.Domain.Messages;

namespace TravelBookingPlatform.Application.RoomClasses.AddImageToGallery;

public class AddImageToRoomClassGalleryCommandHandler : IRequestHandler<AddImageToRoomClassGalleryCommand>
{
  private readonly IImageRepository _imageRepository;
  private readonly IRoomClassRepository _roomClassRepository;
  private readonly IUnitOfWork _unitOfWork;

  public AddImageToRoomClassGalleryCommandHandler(
    IImageRepository imageRepository,
    IUnitOfWork unitOfWork,
    IRoomClassRepository roomClassRepository)
  {
    _imageRepository = imageRepository;
    _unitOfWork = unitOfWork;
    _roomClassRepository = roomClassRepository;
  }
  
  public async Task Handle(AddImageToRoomClassGalleryCommand request,
    CancellationToken cancellationToken = default)
  {
    if (!await _roomClassRepository.ExistsAsync(rc => rc.Id == request.RoomClassId, cancellationToken))
    {
      throw new NotFoundException(RoomCategoryMessages.NotFoundById);
    }

    await _imageRepository.CreateAsync(
      request.Image,
      request.RoomClassId,
      ImageType.Gallery,
      cancellationToken);

    await _unitOfWork.SaveChangesAsync(cancellationToken);
  }
}