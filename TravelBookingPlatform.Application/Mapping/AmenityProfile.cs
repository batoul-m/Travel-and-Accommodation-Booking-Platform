using AutoMapper;
using TravelBookingPlatform.Application.Amenities.Create;
using TravelBookingPlatform.Application.Amenities;
using TravelBookingPlatform.Domain.Entities;
using TravelBookingPlatform.Domain.Model;

namespace TravelBookingPlatform.Application.Mapping;

public class AmenityProfile : Profile
{
  public AmenityProfile()
  {
    CreateMap<PaginatedResult<Amenity>, PaginatedResult<AmenityResponse>>()
      .ForMember(dst => dst.Items, options => options.MapFrom(src => src.Items));
    CreateMap<Amenity, AmenityResponse>();
    CreateMap<UpdateAmenity, Amenity>();
    CreateMap<CreateAmenity, Amenity>();
  }
}