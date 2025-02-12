using FluentValidation;
using TravelBookingPlatform.Api.Dtos.Reviews;
using static TravelBookingPlatform.Domain.Constants.Common;
using static TravelBookingPlatform.Domain.Constants.HotelRating;

namespace TravelBookingPlatform.Api.Validators.Reviews;

public class ReviewUpdateRequestValidator : AbstractValidator<ReviewUpdateRequest>
{
  public ReviewUpdateRequestValidator()
  {
    RuleFor(x => x.Content)
      .NotEmpty()
      .MaximumLength(TextMaxLength);

    RuleFor(x => x.Rating)
      .NotEmpty()
      .InclusiveBetween(MinStarRating, MaxStarRating);
  }
}