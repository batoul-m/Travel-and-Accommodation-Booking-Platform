using AutoMapper;
using TravelBookingPlatform.Application.Discounts.Create;
using TravelBookingPlatform.Application.Discounts.GetById;
using TravelBookingPlatform.Domain.Entities;
using TravelBookingPlatform.Domain.Model;

namespace TravelBookingPlatform.Application.Mapping;

public class DiscountProfile : Profile
{
  public DiscountProfile()
  {
    CreateMap<PaginatedResult<Discount>, PaginatedResult<Discount>>()
      .ForMember(dst => dst.Items, options => options.MapFrom(src => src.Items));
    CreateMap<Discount, DiscountResponse>();
    CreateMap<CreateDiscountCommand, Discount>();
  }
}