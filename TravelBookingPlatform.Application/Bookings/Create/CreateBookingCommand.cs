using MediatR;
using TravelBookingPlatform.Domain.Enums;
using TravelBookingPlatform.Application.Bookings.Common;

namespace TravelBookingPlatform.Application.Bookings;

public class CreateBookingCommand : IRequest<BookingResponse>
{
  public IEnumerable<Guid> RoomIds { get; init; }
  public Guid HotelId { get; init; }
  public DateOnly CheckInDateUtc { get; init; }
  public DateOnly CheckOutDateUtc { get; init; }
  public string? GuestRemarks { get; init; }
  public PaymentMethod PaymentMethod { get; init; }
}