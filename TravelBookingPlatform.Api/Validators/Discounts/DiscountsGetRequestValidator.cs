using FluentValidation;
using TravelBookingPlatform.Api.Dtos.Discounts;
using TravelBookingPlatform.Api.Validators.Common;

namespace TravelBookingPlatform.Api.Validators.Discounts;

public class DiscountsGetRequestValidator : AbstractValidator<DiscountsGetRequest>
{
  public DiscountsGetRequestValidator()
  {
    Include(new ResourcesQueryRequestValidator());

    RuleFor(x => x.SortColumn)
      .Must(BeAValidSortColumn)
      .WithMessage(DiscountsValidationMessages.ShouldBeAValidSortColumn);
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
      "creationDate",
      "startDate",
      "endDate"
    };

    return Array.Exists(validColumns,
      col => string.Equals(col, sortColumn, StringComparison.OrdinalIgnoreCase));
  }
}