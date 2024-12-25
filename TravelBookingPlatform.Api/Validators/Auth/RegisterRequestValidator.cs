using FluentValidation;
using TravelBookingPlatform.Api.Dtos.Auth;
using TravelBookingPlatform.Application.Extensions.Validation;
using static TravelBookingPlatform.Domain.Constants.Common;

namespace TravelBookingPlatform.Api.Validators.Auth;

public class RegisterRequestValidator : AbstractValidator<RegisterRequest>
{
  public RegisterRequestValidator()
  {
    RuleFor(x => x.FirstName)
      .NotEmpty()
      .ValidName(MinNameLength, MaxNameLength);

    RuleFor(x => x.LastName)
      .NotEmpty()
      .ValidName(MinNameLength, MaxNameLength);

    RuleFor(x => x.Email)
      .NotEmpty()
      .EmailAddress();

    RuleFor(x => x.Password)
      .NotEmpty()
      .StrongPassword();
  }
}