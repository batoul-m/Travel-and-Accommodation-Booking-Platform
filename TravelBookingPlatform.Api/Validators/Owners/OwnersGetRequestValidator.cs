﻿using FluentValidation;
using TravelBookingPlatform.Api.Dtos.Owners;
using TravelBookingPlatform.Api.Validators.Common;
using static TravelBookingPlatform.Domain.Constants.Common;

namespace TravelBookingPlatform.Api.Validators.Owners;

public class OwnersGetRequestValidator : AbstractValidator<OwnersGetRequest>
{
  public OwnersGetRequestValidator()
  {
    Include(new ResourcesQueryRequestValidator());
    
    RuleFor(x => x.SearchTerm)
      .MaximumLength(ShortTextMaxLength);

    RuleFor(x => x.SortColumn)
      .Must(BeAValidSortColumn)
      .WithMessage(OwnerValidationMessages.SortColumnNotValid);
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
      "FirstName",
      "LastName"
    };

    return Array.Exists(validColumns,
      col => string.Equals(col, sortColumn, StringComparison.OrdinalIgnoreCase));
  }
}