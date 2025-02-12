using FluentValidation;
using TravelBookingPlatform.Api.Dtos.Common;
using static TravelBookingPlatform.Domain.Constants.Common;

namespace TravelBookingPlatform.Api.Validators.Common;

public class ResourcesQueryRequestValidator : AbstractValidator<ResourcesQueryRequest>
{
  public ResourcesQueryRequestValidator()
  {
    RuleFor(x => x.SortOrder)
      .Must(BeAValidSortOrder)
      .WithMessage(CommonValidationMessages.SortOrderNotValid);

    RuleFor(x => x.PageNumber)
      .NotEmpty()
      .GreaterThanOrEqualTo(1);

    RuleFor(x => x.PageSize)
      .NotEmpty()
      .GreaterThanOrEqualTo(1);
  }

  private static bool BeAValidSortOrder(string? sortOrder)
  {
    if (string.IsNullOrEmpty(sortOrder))
    {
      return true;
    }

    return Array.Exists(["asc", "desc"],
      col => string.Equals(col, sortOrder, StringComparison.OrdinalIgnoreCase));
  }
}