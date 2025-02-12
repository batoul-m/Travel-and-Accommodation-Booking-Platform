using NReco.PdfGenerator;
using TravelBookingPlatform.Domain.Interfaces.Services;

namespace TravelBookingPlatform.Infrastructure.Services.Pdf;

public class PdfService : IPdfService
{
  public async Task<byte[]> GeneratePdfFromHtmlAsync(string html, CancellationToken cancellationToken = default)
  {
    return await Task.Run(() =>
    {
      var htmlToPdfConverter = new HtmlToPdfConverter();

      return htmlToPdfConverter.GeneratePdf(html);
    }, cancellationToken);
  }
}