using AutoMapper;
using TravelBookingPlatform.Api.Dtos.Rooms;
using TravelBookingPlatform.Application.Rooms.Create;
using TravelBookingPlatform.Application.Rooms.GetByRoomClassIdForGuest;
using TravelBookingPlatform.Application.Rooms.GetForManagement;
using TravelBookingPlatform.Application.Rooms.Update;
using static TravelBookingPlatform.Api.Mapping.MappingUtilities;

namespace TravelBookingPlatform.Api.Mapping;

public class RoomProfile : Profile
{
  public RoomProfile()
  {
    CreateMap<RoomsGetRequest, GetRoomsForManagementQuery>()
      .ForMember(dst => dst.SortOrder, opt => opt.MapFrom(src => MapToSortOrder(src.SortOrder)));

    CreateMap<RoomsForGuestsGetRequest, GetRoomsByRoomClassIdForGuestsQuery>();

    CreateMap<RoomCreationRequest, CreateRoomCommand>();

    CreateMap<RoomUpdateRequest, UpdateRoomCommand>();
  }
}