using AutoMapper;
using TravelBookingPlatform.Api.Dtos.Images;
using TravelBookingPlatform.Application.Cities.SetThumbnail;
using TravelBookingPlatform.Application.Hotels.AddToGallery;
using TravelBookingPlatform.Application.Hotels.SetThumbnail;
using TravelBookingPlatform.Application.RoomClasses.AddImageToGallery;

namespace TravelBookingPlatform.Api.Mapping;

public class ImageProfile : Profile
{
  public ImageProfile()
  {
    CreateMap<ImageCreationRequest, SetCityThumbnailCommand>();
    CreateMap<ImageCreationRequest, SetHotelThumbnailCommand>();
    CreateMap<ImageCreationRequest, AddImageToHotelGalleryCommand>();
    CreateMap<ImageCreationRequest, AddImageToRoomClassGalleryCommand>();
  }
}