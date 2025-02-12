using FluentValidation;
using TravelBookingPlatform.Api.Dtos.Amenities;
using TravelBookingPlatform.Api.Validators.Common;
using static TravelBookingPlatform.Domain.Constants.Common;

namespace TravelBookingPlatform.Api.Validators.Amenities;

public class AmenitiesGetRequestValidator : AbstractValidator<AmenitiesGetRequest>
{
  public AmenitiesGetRequestValidator()
  {
    Include(new ResourcesQueryRequestValidator());
    
    RuleFor(x => x.SearchTerm)
      .MaximumLength(ShortTextMaxLength);

    RuleFor(x => x.SortColumn)
      .Must(BeAValidSortColumn)
      .WithMessage(AmenitiesValidationMessages.ShouldBeAValidSortColumn);
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
      "Name"
    };

    return Array.Exists(validColumns,
      col => string.Equals(col, sortColumn, StringComparison.OrdinalIgnoreCase));
  }
}