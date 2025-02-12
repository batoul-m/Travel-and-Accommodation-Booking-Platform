using FluentValidation;

namespace TravelBookingPlatform.Infrastructure.Auth;

public class JwtAuthConfigValidator : AbstractValidator<JwtAuthConfig>
{
  public JwtAuthConfigValidator()
  {
    RuleFor(x => x.Issuer)
        .NotEmpty()
        .WithMessage("Issuer is required.");
        
    RuleFor(x => x.Audience)
        .NotEmpty()
        .WithMessage("Audience is required.");

    RuleFor(x => x.SecretKey)
        .NotEmpty()
        .WithMessage("SecretKey is required.");

    RuleFor(x => x.TokenLifetimeMinutes)
        .GreaterThan(0)
        .WithMessage("TokenLifetimeMinutes must be greater than zero.");
  }
}