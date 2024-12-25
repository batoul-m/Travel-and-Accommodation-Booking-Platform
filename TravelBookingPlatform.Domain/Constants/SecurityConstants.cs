namespace TravelBookingPlatform.Domain.Constants;

public static class SecurityConstants
{
    public const string JwtSecret = "your-secure-key";
    public const int TokenExpirationHours = 24;
    public const string DefaultPasswordPolicy = "Password must include...";
}
