using FluentValidation;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.Extensions.Options;
using TravelBookingPlatform.Shared.OptionsValidation;
using static TravelBookingPlatform.Api.RateLimiting.Constants;

namespace TravelBookingPlatform.Api.RateLimiting;

public static class RateLimitingConfiguration
{
  public static IServiceCollection AddRateLimiting(this IServiceCollection services)
  {
    services.AddScoped<IValidator<FixedWindowRateLimiterConfig>, FixedWindowRateLimiterConfigValidator>();
    
    services.AddOptions<FixedWindowRateLimiterConfig>()
      .BindConfiguration(nameof(FixedWindowRateLimiterConfig))
      .ValidateOnStart();
    
    services.AddRateLimiter(options =>
    {
      using var scope = services.BuildServiceProvider().CreateScope();
    
      var rateLimiterOptions = scope.ServiceProvider
        .GetRequiredService<IOptions<FixedWindowRateLimiterConfig>>().Value;
      
      options.RejectionStatusCode = StatusCodes.Status429TooManyRequests;
    
      options.AddFixedWindowLimiter(RateLimiterPolicy, limiterOptions =>
      {
        limiterOptions.PermitLimit = rateLimiterOptions.PermitLimit;
        limiterOptions.Window = TimeSpan.FromSeconds(rateLimiterOptions.TimeWindowSeconds);
        limiterOptions.QueueProcessingOrder = rateLimiterOptions.QueueProcessingOrder;
        limiterOptions.QueueLimit = rateLimiterOptions.QueueLimit;
      });
    });

    return services;
  } 
}