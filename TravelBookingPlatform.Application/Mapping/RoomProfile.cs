using AutoMapper;
using TravelBookingPlatform.Application.Rooms.Create;
using TravelBookingPlatform.Application.Rooms.GetByRoomClassIdForGuest;
using TravelBookingPlatform.Application.Rooms.GetForManagement;
using TravelBookingPlatform.Application.Rooms.Update;
using TravelBookingPlatform.Domain.Entities;
using TravelBookingPlatform.Domain.Model;

namespace TravelBookingPlatform.Application.Mapping;

public class RoomProfile : Profile
{
  public RoomProfile()
  {
    CreateMap<PaginatedResult<Room>, PaginatedResult<RoomForGuestResponse>>()
      .ForMember(dst => dst.Items, options => options.MapFrom(src => src.Items));
    CreateMap<PaginatedResult<RoomInformations>, PaginatedResult<RoomForManagementResponse>>()
      .ForMember(dst => dst.Items, options => options.MapFrom(src => src.Items));
    CreateMap<CreateRoomCommand, Room>();
    CreateMap<UpdateRoomCommand, Room>();
    CreateMap<Room, RoomForGuestResponse>();
    CreateMap<RoomInformations, RoomForManagementResponse>();
  }
}