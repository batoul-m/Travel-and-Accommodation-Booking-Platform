using FluentValidation;
using TravelBookingPlatform.Api.Dtos.Hotels;

namespace TravelBookingPlatform.Api.Validators.Hotels;

public class HotelFeaturedDealsGetRequestValidator : AbstractValidator<HotelFeaturedDealsGetRequest>
{
  public HotelFeaturedDealsGetRequestValidator()
  {
    RuleFor(x => x.Count)
      .InclusiveBetween(1, 100);
  }
}