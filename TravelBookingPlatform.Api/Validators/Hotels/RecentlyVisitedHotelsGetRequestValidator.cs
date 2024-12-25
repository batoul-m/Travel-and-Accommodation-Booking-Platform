using FluentValidation;
using TravelBookingPlatform.Api.Dtos.Hotels;

namespace TravelBookingPlatform.Api.Validators.Hotels;

public class RecentlyVisitedHotelsGetRequestValidator : AbstractValidator<RecentlyVisitedHotelsGetRequest>
{
  public RecentlyVisitedHotelsGetRequestValidator()
  {
    RuleFor(x => x.Count)
      .InclusiveBetween(1, 100);
  }
}