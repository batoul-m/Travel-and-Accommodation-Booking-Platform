using FluentValidation;
using TravelBookingPlatform.Api.Dtos.RoomClasses;
using TravelBookingPlatform.Application.Extensions.Validation;
using static TravelBookingPlatform.Domain.Constants.Common;
using static TravelBookingPlatform.Domain.Constants.RoomCapacity;

namespace TravelBookingPlatform.Api.Validators.RoomClasses;

public class RoomClassUpdateRequestValidator : AbstractValidator<RoomClassUpdateRequest>
{
  public RoomClassUpdateRequestValidator()
  {
    RuleFor(x => x.Name)
      .NotEmpty()
      .ValidName(MinNameLength, MaxNameLength);

    RuleFor(x => x.Description)
      .MaximumLength(ShortTextMaxLength);

    RuleFor(x => x.AdultsCapacity)
      .NotEmpty()
      .InclusiveBetween(MinAdultCapacity, MaxAdultCapacity);

    RuleFor(x => x.ChildrenCapacity)
      .NotNull()
      .InclusiveBetween(MinChildrenCapacity, MaxChildrenCapacity);

    RuleFor(x => x.PricePerNight)
      .NotEmpty()
      .GreaterThan(Zero);
  }
}