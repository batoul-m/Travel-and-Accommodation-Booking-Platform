using AutoMapper;
using TravelBookingPlatform.Application.Owners.Common;
using TravelBookingPlatform.Application.Owners.Create;
using TravelBookingPlatform.Application.Owners.Update;
using TravelBookingPlatform.Domain.Entities;
using TravelBookingPlatform.Domain.Model;

namespace TravelBookingPlatform.Application.Mapping;

public class OwnerProfile : Profile
{
  public OwnerProfile()
  {
    CreateMap<PaginatedResult<Owner>, PaginatedResult<OwnerResponse>>()
      .ForMember(dst => dst.Items, options => options.MapFrom(src => src.Items));
    CreateMap<Owner, OwnerResponse>();
    CreateMap<CreateOwnerCommand, Owner>();
    CreateMap<UpdateOwnerCommand, Owner>();
  }
}