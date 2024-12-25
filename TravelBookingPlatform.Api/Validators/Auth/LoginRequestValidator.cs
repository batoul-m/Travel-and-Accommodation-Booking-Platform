using FluentValidation;
using TravelBookingPlatform.Api.Dtos.Auth;
using TravelBookingPlatform.Application.Extensions.Validation;

namespace TravelBookingPlatform.Api.Validators.Auth;

public class LoginRequestValidator : AbstractValidator<LoginRequest>
{
  public LoginRequestValidator()
  {
    RuleFor(x => x.Email)
      .NotEmpty()
      .EmailAddress();

    RuleFor(x => x.Password)
      .NotEmpty()
      .StrongPassword();
  }
}