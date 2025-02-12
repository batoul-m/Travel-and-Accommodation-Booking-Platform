using FluentValidation;
using TravelBookingPlatform.Api.Dtos.Hotels;
using TravelBookingPlatform.Api.Validators.Common;
using static TravelBookingPlatform.Domain.Constants.Common;

namespace TravelBookingPlatform.Api.Validators.Hotels;

public class HotelsGetRequestValidator : AbstractValidator<HotelsGetRequest>
{
  public HotelsGetRequestValidator()
  {
    Include(new ResourcesQueryRequestValidator());
    
    RuleFor(x => x.SearchTerm)
      .MaximumLength(ShortTextMaxLength);

    RuleFor(x => x.SortColumn)
      .Must(BeAValidSortColumn)
      .WithMessage(HotelValidationMessages.GetSortColumnNotValid);
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