using AutoMapper;
using TravelBookingPlatform.Api.Dtos.Bookings;
using TravelBookingPlatform.Application.Bookings;
using static TravelBookingPlatform.Api.Mapping.MappingUtilities;

namespace TravelBookingPlatform.Api.Mapping;

public class BookingProfile : Profile
{
  public BookingProfile()
  {
    CreateMap<BookingCreationRequest, CreateBookingCommand>();

    CreateMap<BookingsGetRequest, GetBookingsQuery>()
      .ForMember(dst => dst.SortOrder, opt => opt.MapFrom(src => MapToSortOrder(src.SortOrder)));
  }
}