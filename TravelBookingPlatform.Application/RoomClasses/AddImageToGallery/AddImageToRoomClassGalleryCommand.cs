﻿using MediatR;
using Microsoft.AspNetCore.Http;

namespace TravelBookingPlatform.Application.RoomClasses.AddImageToGallery;

public class AddImageToRoomClassGalleryCommand : IRequest
{
  public Guid RoomClassId { get; init; }
  public IFormFile Image { get; init; }
}