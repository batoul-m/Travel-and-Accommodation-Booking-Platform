using AutoMapper;
using TravelBookingPlatform.Api.Dtos.Owners;
using TravelBookingPlatform.Application.Owners.Create;
using TravelBookingPlatform.Application.Owners.Get;
using TravelBookingPlatform.Application.Owners.Update;
using static TravelBookingPlatform.Api.Mapping.MappingUtilities;

namespace TravelBookingPlatform.Api.Mapping;

public class OwnerProfile : Profile
{
  public OwnerProfile()
  {
    CreateMap<OwnersGetRequest, GetOwnersQuery>()
      .ForMember(dst => dst.SortOrder, opt => opt.MapFrom(src => MapToSortOrder(src.SortOrder)));

    CreateMap<OwnerCreationRequest, CreateOwnerCommand>();
    
    CreateMap<OwnerUpdateRequest, UpdateOwnerCommand>();
  }
}