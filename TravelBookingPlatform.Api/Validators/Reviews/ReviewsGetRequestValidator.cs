using FluentValidation;
using TravelBookingPlatform.Api.Dtos.Reviews;
using TravelBookingPlatform.Api.Validators.Common;

namespace TravelBookingPlatform.Api.Validators.Reviews;

public class ReviewsGetRequestValidator : AbstractValidator<ReviewsGetRequest>
{
  public ReviewsGetRequestValidator()
  {
    Include(new ResourcesQueryRequestValidator());
    
    RuleFor(x => x.SortColumn)
      .Must(BeAValidSortColumn)
      .WithMessage(ReviewValidationMessages.SortColumnNotValid);
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
      "rating"
    };

    return Array.Exists(validColumns,
      col => string.Equals(col, sortColumn, StringComparison.OrdinalIgnoreCase));
  }
}