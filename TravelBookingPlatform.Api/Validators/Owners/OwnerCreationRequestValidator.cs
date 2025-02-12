using FluentValidation;
using TravelBookingPlatform.Api.Dtos.Owners;
using TravelBookingPlatform.Application.Extensions.Validation;
using static TravelBookingPlatform.Domain.Constants.Common;

namespace TravelBookingPlatform.Api.Validators.Owners;

public class OwnerCreationRequestValidator : AbstractValidator<OwnerCreationRequest>
{
  public OwnerCreationRequestValidator()
  {
    RuleFor(c => c.FirstName)
      .NotEmpty()
      .ValidName(MinNameLength, MaxNameLength);

    RuleFor(x => x.LastName)
      .NotEmpty()
      .ValidName(MinNameLength, MaxNameLength);

    RuleFor(x => x.PhoneNumber)
      .NotEmpty()
      .PhoneNumber();

    RuleFor(x => x.Email)
      .NotEmpty()
      .EmailAddress();
  }
}