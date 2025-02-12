using AutoMapper;
using TravelBookingPlatform.Api.Dtos.RoomClasses;
using TravelBookingPlatform.Application.RoomClasses.Create;
using TravelBookingPlatform.Application.RoomClasses.GetByHotelIdForGuest;
using TravelBookingPlatform.Application.RoomClasses.GetForManagement;
using TravelBookingPlatform.Application.RoomClasses.Update;
using static TravelBookingPlatform.Api.Mapping.MappingUtilities;

namespace TravelBookingPlatform.Api.Mapping;

public class RoomClassProfile : Profile
{
  public RoomClassProfile()
  {
    CreateMap<GetRoomClassesForGuestRequest, GetRoomClassesByHotelIdForGuestQuery>()
      .ForMember(dst => dst.SortOrder, opt => opt.MapFrom(src => MapToSortOrder(src.SortOrder)));
    
    CreateMap<RoomClassesGetRequest, GetRoomClassesForManagementQuery>()
      .ForMember(dst => dst.SortOrder, opt => opt.MapFrom(src => MapToSortOrder(src.SortOrder)));

    CreateMap<RoomClassCreationRequest, CreateRoomClassCommand>()
      .ForMember(dst => dst.AmenitiesIds, opt => opt.MapFrom(src => src.AmenitiesIds ?? Enumerable.Empty<Guid>()));

    CreateMap<RoomClassUpdateRequest, UpdateRoomClassCommand>();
  }
}