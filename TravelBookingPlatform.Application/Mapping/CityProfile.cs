using AutoMapper;
using TravelBookingPlatform.Application.Cities.Create;
using TravelBookingPlatform.Application.Cities.GetForManagement;
using TravelBookingPlatform.Application.Cities.GetTrending;
using TravelBookingPlatform.Application.Cities.Update;
using TravelBookingPlatform.Domain.Entities;
using TravelBookingPlatform.Domain.Model;

namespace TravelBookingPlatform.Application.Mapping;

public class CityProfile : Profile
{
  public CityProfile()
  {
    CreateMap<PaginatedResult<CityInformations>, PaginatedResult<CityForManagementResponse>>()
      .ForMember(dst => dst.Items, options => options.MapFrom(src => src.Items));
    CreateMap<CreateCityCommand, City>();
    CreateMap<UpdateCityCommand, City>();
    CreateMap<City, CityResponse>();
    CreateMap<City, TrendingCityResponse>()
      .ForMember(dst => dst.ThumbnailUrl, options => options.MapFrom(
        src => src.Thumbnail != null ? src.Thumbnail.Path : null));
    CreateMap<CityInformations, CityForManagementResponse>();
  }
}