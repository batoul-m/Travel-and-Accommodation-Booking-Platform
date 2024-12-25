using AutoMapper;
using TravelBookingPlatform.Api.Dtos.Discounts;
using TravelBookingPlatform.Application.Discounts.Create;
using TravelBookingPlatform.Application.Discounts.Get;
using static TravelBookingPlatform.Api.Mapping.MappingUtilities;

namespace TravelBookingPlatform.Api.Mapping;

public class DiscountProfile : Profile
{
  public DiscountProfile()
  {
    CreateMap<DiscountsGetRequest, GetDiscountsQuery>()
      .ForMember(dst => dst.SortOrder, opt => opt.MapFrom(src => MapToSortOrder(src.SortOrder)));

    CreateMap<DiscountCreationRequest, CreateDiscountCommand>();
  }
}