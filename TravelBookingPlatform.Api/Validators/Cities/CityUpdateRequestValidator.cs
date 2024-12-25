using FluentValidation;
using TravelBookingPlatform.Api.Dtos.Cities;
using TravelBookingPlatform.Application.Extensions.Validation;
using static TravelBookingPlatform.Domain.Constants.Common;
using static TravelBookingPlatform.Domain.Constants.CityPostOffice;

namespace TravelBookingPlatform.Api.Validators.Cities;

public class CityUpdateRequestValidator : AbstractValidator<CityUpdateRequest>
{
  public CityUpdateRequestValidator()
  {
    RuleFor(c => c.Name)
      .NotEmpty()
      .ValidName(MinNameLength, MaxNameLength);

    RuleFor(c => c.Country)
      .NotEmpty()
      .ValidName(MinNameLength, MaxNameLength);

    RuleFor(c => c.PostOffice)
      .NotEmpty()
      .ValidNumericString(PostOfficeLength);
  }
}