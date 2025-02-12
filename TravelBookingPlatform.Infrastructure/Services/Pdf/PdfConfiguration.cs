using Microsoft.Extensions.DependencyInjection;
using TravelBookingPlatform.Domain.Interfaces.Services;

namespace TravelBookingPlatform.Infrastructure.Services.Pdf;

public static class PdfConfiguration
{
  public static IServiceCollection AddPdfInfrastructure(this IServiceCollection services)
  {
    services.AddScoped<IPdfService, PdfService>();

    return services;
  }
}