using AutoMapper;
using TravelBookingPlatform.Application.Hotels.GetFeaturedDeals;
using TravelBookingPlatform.Application.RoomClasses.Create;
using TravelBookingPlatform.Application.RoomClasses.GetByHotelIdForGuest;
using TravelBookingPlatform.Application.RoomClasses.GetForManagement;
using TravelBookingPlatform.Application.RoomClasses.Update;
using TravelBookingPlatform.Domain.Entities;
using TravelBookingPlatform.Domain.Model;

namespace TravelBookingPlatform.Application.Mapping;

public class RoomClassProfile : Profile
{
  public RoomClassProfile()
  {
    CreateMap<PaginatedResult<RoomCategory>, PaginatedResult<RoomClassForGuestResponse>>()
      .ForMember(dst => dst.Items, options => options.MapFrom(src => src.Items));

    CreateMap<PaginatedResult<RoomCategory>, PaginatedResult<RoomClassForManagementResponse>>()
      .ForMember(dst => dst.Items, options => options.MapFrom(src => src.Items));

    CreateMap<CreateRoomClassCommand, RoomCategory>();
    CreateMap<UpdateRoomClassCommand, RoomCategory>();

    CreateMap<RoomCategory, RoomClassForGuestResponse>()
      .ForMember(dst => dst.Amenities, options => options.MapFrom(src => src.Amenities))
      .ForMember(dst => dst.ActiveDiscount, options => options.MapFrom(src => src.Discounts.FirstOrDefault()))
      .ForMember(dst => dst.GalleryUrls, options => options
        .MapFrom(src => src.Gallery.Select(i => i.Path)));

    CreateMap<RoomCategory, RoomClassForManagementResponse>()
      .ForMember(dst => dst.Amenities, options => options.MapFrom(src => src.Amenities))
      .ForMember(dst => dst.ActiveDiscount, options => options.MapFrom(src => src.Discounts.FirstOrDefault()));

    CreateMap<RoomCategory, HotelFeaturedDealResponse>()
      .ForMember(dst => dst.Id, options => options.MapFrom(src => src.HotelId))
      .ForMember(dst => dst.Name, options => options.MapFrom(src => src.Hotel.Name))
      .ForMember(dst => dst.RoomClassName, options => options.MapFrom(src => src.Name))
      .ForMember(dst => dst.StarRating, options => options.MapFrom(src => src.Hotel.StarRating))
      .ForMember(dst => dst.Longitude, options => options.MapFrom(src => src.Hotel.Longitude))
      .ForMember(dst => dst.Latitude, options => options.MapFrom(src => src.Hotel.Latitude))
      .ForMember(dst => dst.CityName, options => options.MapFrom(src => src.Hotel.City.Name))
      .ForMember(dst => dst.Latitude, options => options.MapFrom(src => src.Hotel.City.Country))
      .ForMember(dst => dst.OriginalPricePerNight, options => options.MapFrom(src => src.PricePerNight))
      .ForMember(dst => dst.DiscountPercentage, options => options.MapFrom(src => src.Discounts.First().Percentage))
      .ForMember(dst => dst.DiscountStartDateUtc, options => options.MapFrom(src => src.Discounts.First().StartDateUtc))
      .ForMember(dst => dst.DiscountEndDateUtc, options => options.MapFrom(src => src.Discounts.First().EndDateUtc))
      .ForMember(dst => dst.ThumbnailUrl, options => options
        .MapFrom(src => src.Hotel.Thumbnail == null ? null : src.Hotel.Thumbnail.Path));
  }
}