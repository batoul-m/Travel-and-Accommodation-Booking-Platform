using MediatR;

namespace TravelBookingPlatform.Application.Bookings;

public class DeleteBookingCommand : IRequest
{
  public Guid BookingId { get; init; }
}