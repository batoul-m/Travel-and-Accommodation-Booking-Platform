using AutoMapper;
using TravelBookingPlatform.Api.Dtos.Hotels;
using TravelBookingPlatform.Application.Hotels.Create;
using TravelBookingPlatform.Application.Hotels.GetFeaturedDeals;
using TravelBookingPlatform.Application.Hotels.GetForManagement;
using TravelBookingPlatform.Application.Hotels.GetRecentlyVisited;
using TravelBookingPlatform.Application.Hotels.Search;
using TravelBookingPlatform.Application.Hotels.Update;
using TravelBookingPlatform.Domain.Enums;
using static TravelBookingPlatform.Api.Mapping.MappingUtilities;

namespace TravelBookingPlatform.Api.Mapping;

public class HotelProfile : Profile
{
  public HotelProfile()
  {
    CreateMap<RecentlyVisitedHotelsGetRequest, GetRecentlyVisitedHotelsForGuestQuery>();
    
    CreateMap<HotelsGetRequest, GetHotelsForManagementQuery>()
      .ForMember(dst => dst.SortOrder, opt => opt.MapFrom(src => MapToSortOrder(src.SortOrder)));

    CreateMap<HotelSearchRequest, SearchForHotelsQuery>()
      .ForMember(dst => dst.SortOrder, opt => opt.MapFrom(src => MapToSortOrder(src.SortOrder)))
      .ForMember(dst => dst.RoomTypes, opt => opt.MapFrom(src => src.RoomTypes ?? Enumerable.Empty<RoomType>()))
      .ForMember(dst => dst.Amenities, opt => opt.MapFrom(src => src.Amenities ?? Enumerable.Empty<Guid>()));

    CreateMap<HotelFeaturedDealsGetRequest, GetHotelFeaturedDealsQuery>();

    CreateMap<HotelCreationRequest, CreateHotelCommand>();

    CreateMap<HotelUpdateRequest, UpdateHotelCommand>();
  }
}