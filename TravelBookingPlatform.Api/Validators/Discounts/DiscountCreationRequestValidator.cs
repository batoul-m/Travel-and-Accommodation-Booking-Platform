using FluentValidation;
using TravelBookingPlatform.Api.Dtos.Discounts;
using static TravelBookingPlatform.Domain.Constants.Discounts;

namespace TravelBookingPlatform.Api.Validators.Discounts;

public class DiscountCreationRequestValidator : AbstractValidator<DiscountCreationRequest>
{
  public DiscountCreationRequestValidator()
  {
    RuleFor(x => x.Percentage)
      .NotEmpty()
      .GreaterThan(MinDiscountPercentage)
      .LessThanOrEqualTo(MaxDiscountPercentage);

    RuleFor(x => x.StartDateUtc)
      .NotEmpty()
      .GreaterThan(DateTime.UtcNow);

    RuleFor(x => x.EndDateUtc)
      .NotEmpty()
      .GreaterThan(x => x.StartDateUtc);
  }
}