using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.DependencyInjection;
using TravelBookingPlatform.Domain.Interfaces.Services;
using Microsoft.Extensions.Configuration;
using TravelBookingPlatform.Shared.OptionsValidation; 

namespace TravelBookingPlatform.Infrastructure.Services.Email;

public static class EmailConfiguration
{
  public static IServiceCollection AddEmailInfrastructure(this IServiceCollection services)
  {
    services.AddScoped<IValidator<EmailConfig>, EmailConfigValidator>();

    services.AddOptions<EmailConfig>()
      .BindConfiguration(nameof(EmailConfig))
      .AddFluentValidation()
      .ValidateOnStart();

   services.AddTransient<IEmailService, EmailService>();

    return services;
  }
}