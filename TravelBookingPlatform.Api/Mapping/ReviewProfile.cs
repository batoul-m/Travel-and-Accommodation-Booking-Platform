using AutoMapper;
using TravelBookingPlatform.Api.Dtos.Reviews;
using TravelBookingPlatform.Application.Reviews.Create;
using TravelBookingPlatform.Application.Reviews.GetByHotelId;
using TravelBookingPlatform.Application.Reviews.Update;
using static TravelBookingPlatform.Api.Mapping.MappingUtilities;

namespace TravelBookingPlatform.Api.Mapping;

public class ReviewProfile : Profile
{
  public ReviewProfile()
  {
    CreateMap<ReviewsGetRequest, GetReviewsByHotelIdQuery>()
      .ForMember(dst => dst.SortOrder, opt => opt.MapFrom(src => MapToSortOrder(src.SortOrder)));

    CreateMap<ReviewCreationRequest, CreateReviewCommand>();

    CreateMap<ReviewUpdateRequest, UpdateReviewCommand>();
  }
}