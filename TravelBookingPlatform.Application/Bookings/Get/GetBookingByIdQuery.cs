using MediatR;
using TravelBookingPlatform.Application.Bookings.Common;

namespace TravelBookingPlatform.Application.Bookings;

public class GetBookingByIdQuery : IRequest<BookingResponse>
{
  public Guid BookingId { get; init; }
}