﻿using MediatR;
using TravelBookingPlatform.Domain;
using TravelBookingPlatform.Domain.Enums;
using TravelBookingPlatform.Domain.Exceptions;
using TravelBookingPlatform.Domain.Interfaces.Persistence;
using TravelBookingPlatform.Domain.Interfaces.Persistence.Repositories;
using TravelBookingPlatform.Domain.Messages;
using TravelBookingPlatform.Application.Hotels.SetThumbnail;

namespace TravelBookingPlatform.Application.Hotels.SetThumbnail;

public class SetHotelThumbnailCommandHandler : IRequestHandler<SetHotelThumbnailCommand>
{
  private readonly IHotelRepository _hotelRepository;
  private readonly IImageRepository _imageRepository;
  private readonly IUnitOfWork _unitOfWork;

  public SetHotelThumbnailCommandHandler(
    IImageRepository imageRepository,
    IUnitOfWork unitOfWork,
    IHotelRepository hotelRepository)
  {
    _imageRepository = imageRepository;
    _unitOfWork = unitOfWork;
    _hotelRepository = hotelRepository;
  }

  public async Task Handle(SetHotelThumbnailCommand request,
    CancellationToken cancellationToken)
  {
    if (!await _hotelRepository.ExistsAsync(h => h.Id == request.HotelId, cancellationToken))
    {
      throw new NotFoundException(HotelMessages.NotFoundById);
    }

    await _imageRepository.DeleteForAsync(
      request.HotelId,
      ImageType.Thumbnail,
      cancellationToken);

    await _imageRepository.CreateAsync(
      request.Image,
      request.HotelId,
      ImageType.Thumbnail,
      cancellationToken);

    await _unitOfWork.SaveChangesAsync(cancellationToken);
  }
}