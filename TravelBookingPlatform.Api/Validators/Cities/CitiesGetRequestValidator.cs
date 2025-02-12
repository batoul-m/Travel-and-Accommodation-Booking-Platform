using FluentValidation;
using TravelBookingPlatform.Api.Dtos.Cities;
using TravelBookingPlatform.Api.Validators.Common;
using static TravelBookingPlatform.Domain.Constants.Common;

namespace TravelBookingPlatform.Api.Validators.Cities;

public class CitiesGetRequestValidator : AbstractValidator<CitiesGetRequest>
{
  public CitiesGetRequestValidator()
  {
    Include(new ResourcesQueryRequestValidator());
    
    RuleFor(x => x.SearchTerm)
      .MaximumLength(ShortTextMaxLength);

    RuleFor(x => x.SortColumn)
      .Must(BeAValidSortColumn)
      .WithMessage(CitiesValidationMessages.ShouldBeAValidSortColumn);
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
      "Country",
      "PostOffice"
    };

    return Array.Exists(validColumns,
      col => string.Equals(col, sortColumn, StringComparison.OrdinalIgnoreCase));
  }
}