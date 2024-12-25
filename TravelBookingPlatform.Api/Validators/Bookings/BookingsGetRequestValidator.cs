using FluentValidation;
using TravelBookingPlatform.Api.Dtos.Bookings;
using TravelBookingPlatform.Api.Validators.Common;

namespace TravelBookingPlatform.Api.Validators.Bookings;

public class BookingsGetRequestValidator : AbstractValidator<BookingsGetRequest>
{
  public BookingsGetRequestValidator()
  {
    Include(new ResourcesQueryRequestValidator());
    
    RuleFor(x => x.SortColumn)
      .Must(BeAValidSortColumn)
      .WithMessage(BookingsValidationMessages.SortColumnNotValid);
  }
  
  private static bool BeAValidSortColumn(string? sortColumn)
  {
    if (string.IsNullOrEmpty(sortColumn))
    {
      return true;
    }

    var validColumns = new[]
    {
      "id",
      "bookingDate",
      "checkInDate",
      "checkOutDate"
    };

    return Array.Exists(validColumns,
      col => string.Equals(col, sortColumn, StringComparison.OrdinalIgnoreCase));
  }
}