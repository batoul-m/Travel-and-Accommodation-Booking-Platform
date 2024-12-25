using TravelBookingPlatform.Domain.Interfaces.Services;

namespace TravelBookingPlatform.Infrastructure.Services.Date;

public class DateTimeProvider : IDateTimeProvider
{
  public DateTime GetCurrentDateTimeUtc()
  {
    return DateTime.UtcNow;
  }

  public DateOnly GetCurrentDateUtc()
  {
    return DateOnly.FromDateTime(DateTime.UtcNow);
  }
}