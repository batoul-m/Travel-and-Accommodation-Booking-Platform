using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TravelBookingPlatform.Domain.Interfaces.Services;
using TravelBookingPlatform.Infrastructure.Auth;
using TravelBookingPlatform.Infrastructure.Persistence;
using TravelBookingPlatform.Infrastructure.Services.Date;
using TravelBookingPlatform.Infrastructure.Services.Email;
using TravelBookingPlatform.Infrastructure.Services.Pdf;

namespace TravelBookingPlatform.Infrastructure;

public static class InfrastructureServiceExtensions
{
  public static IServiceCollection AddInfrastructure(
    this IServiceCollection services, 
    IConfiguration config)
  {
    services.AddPersistenceInfrastructure(config)
      .AddAuthInfrastructure()
      .AddEmailInfrastructure()
      .AddPdfInfrastructure()
      .AddTransient<IDateTimeProvider, DateTimeProvider>();

    return services;
  }
}