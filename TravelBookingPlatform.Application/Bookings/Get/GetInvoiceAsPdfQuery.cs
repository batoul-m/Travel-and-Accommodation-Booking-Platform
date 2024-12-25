using MediatR;

namespace TravelBookingPlatform.Application.Bookings;

public class GetInvoiceAsPdfQuery : IRequest<byte[]>
{
  public Guid BookingId { get; init; }
}