using MediatR;
using TravelBookingPlatform.Domain.Enums;
using TravelBookingPlatform.Domain.Exceptions;
using TravelBookingPlatform.Domain.Interfaces.Persistence;
using TravelBookingPlatform.Domain.Interfaces.Persistence.Repositories;
using TravelBookingPlatform.Domain.Messages;

namespace TravelBookingPlatform.Application.Hotels.AddToGallery;

public class AddImageToHotelGalleryCommandHandler : IRequestHandler<AddImageToHotelGalleryCommand>
{
  private readonly IHotelRepository _hotelRepository;
  private readonly IImageRepository _imageRepository;
  private readonly IUnitOfWork _unitOfWork;

  public AddImageToHotelGalleryCommandHandler(
    IImageRepository imageRepository,
    IUnitOfWork unitOfWork,
    IHotelRepository hotelRepository)
  {
    _imageRepository = imageRepository;
    _unitOfWork = unitOfWork;
    _hotelRepository = hotelRepository;
  }

  public async Task Handle(AddImageToHotelGalleryCommand request,
    CancellationToken cancellationToken = default)
  {
    if (!await _hotelRepository.ExistsAsync(h => h.Id == request.HotelId, cancellationToken))
    {
      throw new NotFoundException(HotelMessages.NotFoundById);
    }

    await _imageRepository.CreateAsync(
      request.Image,
      request.HotelId,
      ImageType.Gallery,
      cancellationToken);

    await _unitOfWork.SaveChangesAsync(cancellationToken);
  }
}