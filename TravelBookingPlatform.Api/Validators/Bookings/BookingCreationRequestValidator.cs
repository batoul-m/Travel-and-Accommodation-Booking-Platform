using FluentValidation;
using TravelBookingPlatform.Api.Dtos.Bookings;

namespace TravelBookingPlatform.Api.Validators.Bookings;

public class BookingCreationRequestValidator : AbstractValidator<BookingCreationRequest>
{
  public BookingCreationRequestValidator()
  {
    RuleFor(x => x.RoomIds)
      .NotEmpty();

    RuleFor(b => b.CheckInDateUtc)
      .NotEmpty()
      .GreaterThanOrEqualTo(DateOnly.FromDateTime(DateTime.UtcNow));

    RuleFor(b => b.CheckOutDateUtc)
      .NotEmpty()
      .GreaterThanOrEqualTo(b => b.CheckInDateUtc);

    RuleFor(x => x.PaymentMethod)
      .NotEmpty()
      .IsInEnum();
  }
}