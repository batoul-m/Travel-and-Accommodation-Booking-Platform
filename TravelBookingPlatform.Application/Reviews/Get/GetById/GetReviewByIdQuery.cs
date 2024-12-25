using MediatR;
using TravelBookingPlatform.Application.Reviews.Common;

namespace TravelBookingPlatform.Application.Reviews.GetById;

public class GetReviewByIdQuery : IRequest<ReviewResponse>
{
  public Guid HotelId { get; init; }
  public Guid ReviewId { get; init; }
}