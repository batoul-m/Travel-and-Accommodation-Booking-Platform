using FluentValidation;
using TravelBookingPlatform.Api.Dtos.Rooms;
using TravelBookingPlatform.Api.Validators.Common;

namespace TravelBookingPlatform.Api.Validators.Rooms;

public class RoomsForGuestsGetRequestValidator : AbstractValidator<RoomsForGuestsGetRequest>
{
  public RoomsForGuestsGetRequestValidator()
  {
    Include(new ResourcesQueryRequestValidator());

    RuleFor(x => x.CheckInDateUtc)
      .GreaterThanOrEqualTo(DateOnly.FromDateTime(DateTime.UtcNow));

    RuleFor(x => x.CheckOutDateUtc)
      .GreaterThanOrEqualTo(x => x.CheckInDateUtc);
  }
}