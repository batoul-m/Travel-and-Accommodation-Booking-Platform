using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using TravelBookingPlatform.Domain.Interfaces.Persistence.Services;
using TravelBookingPlatform.Shared.OptionsValidation;

namespace TravelBookingPlatform.Infrastructure.Persistence.Services.Images;

public static class ImageServiceConfiguration
{
  public static IServiceCollection AddImageService(this IServiceCollection services)
  {
    services.AddScoped<IValidator<FirebaseConfig>, FireBaseConfigValidator>();

    services.AddOptions<FirebaseConfig>()
      .BindConfiguration(nameof(FirebaseConfig))
      .AddFluentValidation()
      .ValidateOnStart();
    
    services.AddScoped<IImageService, FirebaseImageService>();

    return services;
  }
}