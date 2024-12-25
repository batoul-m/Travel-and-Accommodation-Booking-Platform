namespace TravelBookingPlatform.Infrastructure.Auth;

public class JwtAuthConfig
{
  public required string SecretKey { get; set; }

  public required string Issuer { get; set; }

  public required string Audience { get; set; }

  public required double TokenLifetimeMinutes { get; set; }
}