using FluentValidation;
using TravelBookingPlatform.Api.Dtos.Amenities;
using TravelBookingPlatform.Application.Extensions.Validation;
using static TravelBookingPlatform.Domain.Constants.Common;

namespace TravelBookingPlatform.Api.Validators.Amenities;

public class AmenityUpdateRequestValidator : AbstractValidator<AmenityUpdateRequest>
{
  public AmenityUpdateRequestValidator()
  {
    RuleFor(x => x.Name)
      .NotEmpty()
      .ValidName(MinNameLength, MaxNameLength);

    RuleFor(x => x.Description)
      .MaximumLength(ShortTextMaxLength);
  }
}