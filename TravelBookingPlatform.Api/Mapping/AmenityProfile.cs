using AutoMapper;
using TravelBookingPlatform.Api.Dtos.Amenities;
using TravelBookingPlatform.Application.Amenities.Create;
using TravelBookingPlatform.Application.Amenities;
using static TravelBookingPlatform.Api.Mapping.MappingUtilities;

namespace TravelBookingPlatform.Api.Mapping;

public class AmenityProfile : Profile
{
  public AmenityProfile()
  {
    CreateMap<AmenitiesGetRequest, GetAmenitiesQuery>()
      .ForMember(dst => dst.SortOrder, opt => opt.MapFrom(src => MapToSortOrder(src.SortOrder)));

    CreateMap<AmenityCreationRequest, CreateAmenity>();

    CreateMap<AmenityUpdateRequest, UpdateAmenity>();
  }
}