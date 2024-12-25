using FluentValidation;
using TravelBookingPlatform.Api.Dtos.RoomClasses;
using TravelBookingPlatform.Api.Validators.Common;

namespace TravelBookingPlatform.Api.Validators.RoomClasses;

public class GetRoomClassesForGuestRequestValidator : AbstractValidator<GetRoomClassesForGuestRequest>
{
  public GetRoomClassesForGuestRequestValidator()
  {
    Include(new ResourcesQueryRequestValidator());
    
    RuleFor(x => x.SortColumn)
      .Must(BeAValidSortColumn)
      .WithMessage(RoomClassesValidationMessages.SortColumnNotValid);
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
      "Name",
      "AdultsCapacity",
      "ChildrenCapacity",
      "PricePerNight"
    };

    return Array.Exists(validColumns,
      col => string.Equals(col, sortColumn, StringComparison.OrdinalIgnoreCase));
  }
}