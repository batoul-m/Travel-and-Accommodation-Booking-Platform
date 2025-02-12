using AutoMapper;
using TravelBookingPlatform.Api.Dtos.Cities;
using TravelBookingPlatform.Application.Cities.Create;
using TravelBookingPlatform.Application.Cities.GetForManagement;
using TravelBookingPlatform.Application.Cities.GetTrending;
using TravelBookingPlatform.Application.Cities.Update;
using static TravelBookingPlatform.Api.Mapping.MappingUtilities;

namespace TravelBookingPlatform.Api.Mapping;

public class CityProfile : Profile
{
  public CityProfile()
  {
    CreateMap<CitiesGetRequest, GetCitiesForManagementQuery>()
      .ForMember(dst => dst.SortOrder, opt => opt.MapFrom(src => MapToSortOrder(src.SortOrder)));

    CreateMap<TrendingCitiesGetRequest, GetTrendingCitiesQuery>();

    CreateMap<CityCreationRequest, CreateCityCommand>();

    CreateMap<CityUpdateRequest, UpdateCityCommand>();
  }
}