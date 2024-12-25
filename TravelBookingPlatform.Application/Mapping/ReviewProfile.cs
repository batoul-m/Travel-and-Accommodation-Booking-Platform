using AutoMapper;
using TravelBookingPlatform.Application.Reviews.Common;
using TravelBookingPlatform.Application.Reviews.Create;
using TravelBookingPlatform.Application.Reviews.Update;
using TravelBookingPlatform.Domain.Entities;
using TravelBookingPlatform.Domain.Model;

namespace TravelBookingPlatform.Application.Mapping;

public class ReviewProfile : Profile
{
  public ReviewProfile()
  {
    CreateMap<CreateReviewCommand, Review>();
    CreateMap<UpdateReviewCommand, Review>();
    CreateMap<Review, ReviewResponse>();
    CreateMap<PaginatedResult<Review>, PaginatedResult<ReviewResponse>>()
      .ForMember(dst => dst.Items, options => options.MapFrom(src => src.Items));
  }
}