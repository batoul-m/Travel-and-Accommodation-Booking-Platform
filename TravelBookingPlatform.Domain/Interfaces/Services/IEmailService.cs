using TravelBookingPlatform.Domain.Model;

namespace TravelBookingPlatform.Domain.Interfaces.Services;

public interface IEmailService
{
  Task SendAsync(EmailRequest emailRequest, CancellationToken cancellationToken = default);
}