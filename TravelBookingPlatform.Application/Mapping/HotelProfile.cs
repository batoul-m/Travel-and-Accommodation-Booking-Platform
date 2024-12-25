using AutoMapper;
using TravelBookingPlatform.Application.Hotels.Create;
using TravelBookingPlatform.Application.Hotels.GetForGuestById;
using TravelBookingPlatform.Application.Hotels.GetForManagement;
using TravelBookingPlatform.Application.Hotels.Search;
using TravelBookingPlatform.Application.Hotels.Update;
using TravelBookingPlatform.Domain.Entities;
using TravelBookingPlatform.Domain.Model;

namespace TravelBookingPlatform.Application.Mapping;

public class HotelProfile : Profile
{
  public HotelProfile()
  {
    CreateMap<PaginatedResult<HotelSearchResult>, PaginatedResult<HotelSearchResultResponse>>()
      .ForMember(dst => dst.Items, options => options.MapFrom(src => src.Items));

    CreateMap<PaginatedResult<HotelInformations>, PaginatedResult<HotelForManagementResponse>>()
      .ForMember(dst => dst.Items, options => options.MapFrom(src => src.Items));

    CreateMap<CreateHotelCommand, Hotel>();
    CreateMap<UpdateHotelCommand, Hotel>();

    CreateMap<Hotel, HotelForGuestResponse>()
      .ForMember(dst => dst.ThumbnailUrl, options => options.MapFrom(
        src => src.Thumbnail != null ? src.Thumbnail.Path : null))
      .ForMember(dst => dst.GalleryUrls, options => options.MapFrom(
        src => src.Gallery.Select(i => i.Path)));

    CreateMap<HotelSearchResult, HotelSearchResultResponse>()
      .ForMember(dst => dst.ThumbnailUrl, options => options.MapFrom(
        src => src.Thumbnail != null ? src.Thumbnail.Path : null));

    CreateMap<HotelInformations, HotelForManagementResponse>()
      .ForMember(dst => dst.Owner, options => options.MapFrom(src => src.Owner));
  }
}