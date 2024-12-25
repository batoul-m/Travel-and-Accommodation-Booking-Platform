using FluentValidation;
using TravelBookingPlatform.Api.Dtos.Rooms;
using TravelBookingPlatform.Api.Validators.Common;
using static TravelBookingPlatform.Domain.Constants.Common;

namespace TravelBookingPlatform.Api.Validators.Rooms;

public class RoomsGetRequestValidator : AbstractValidator<RoomsGetRequest>
{
  public RoomsGetRequestValidator()
  {
    Include(new ResourcesQueryRequestValidator());
    
    RuleFor(x => x.SearchTerm)
      .MaximumLength(ShortTextMaxLength);

    RuleFor(x => x.SortColumn)
      .Must(BeAValidSortColumn)
      .WithMessage(RoomValidationMessages.SortColumnNotValid);
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
      "number"
    };

    return Array.Exists(validColumns,
      col => string.Equals(col, sortColumn, StringComparison.OrdinalIgnoreCase));
  }
}