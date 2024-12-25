using System.Numerics;
using FluentValidation;
using TravelBookingPlatform.Api.Dtos.RoomClasses;
using TravelBookingPlatform.Application.Extensions.Validation;
using static TravelBookingPlatform.Domain.Constants.Common;
using static TravelBookingPlatform.Domain.Constants.RoomCapacity;

namespace TravelBookingPlatform.Api.Validators.RoomClasses;

public class RoomClassCreationRequestValidator : AbstractValidator<RoomClassCreationRequest>
{
  public RoomClassCreationRequestValidator()
  {
    RuleFor(x => x.HotelId)
      .NotEmpty();

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