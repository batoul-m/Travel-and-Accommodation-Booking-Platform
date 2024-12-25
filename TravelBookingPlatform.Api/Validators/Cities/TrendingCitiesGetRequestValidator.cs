using FluentValidation;
using TravelBookingPlatform.Api.Dtos.Cities;

namespace TravelBookingPlatform.Api.Validators.Cities;

public class TrendingCitiesGetRequestValidator : AbstractValidator<TrendingCitiesGetRequest>
{
  public TrendingCitiesGetRequestValidator()
  {
    RuleFor(x => x.Count)
      .InclusiveBetween(1, 100);
  }
}