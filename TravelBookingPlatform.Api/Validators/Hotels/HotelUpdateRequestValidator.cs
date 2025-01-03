﻿using FluentValidation;
using TravelBookingPlatform.Api.Dtos.Hotels;
using TravelBookingPlatform.Application.Extensions.Validation;
using static TravelBookingPlatform.Domain.Constants.Common;
using static TravelBookingPlatform.Domain.Constants.HotelRating;
using static TravelBookingPlatform.Domain.Constants.Location;

namespace TravelBookingPlatform.Api.Validators.Hotels;

public class HotelUpdateRequestValidator : AbstractValidator<HotelUpdateRequest>
{
  public HotelUpdateRequestValidator()
  {
    RuleFor(x => x.CityId)
      .NotEmpty();

    RuleFor(x => x.OwnerId)
      .NotEmpty();

    RuleFor(x => x.Name)
      .NotEmpty()
      .ValidName(MinNameLength, MaxNameLength);

    RuleFor(x => x.StarRating)
      .NotEmpty()
      .InclusiveBetween(MinStarRating, MaxStarRating);

    RuleFor(x => x.Longitude)
      .NotEmpty()
      .InclusiveBetween(MinLongitude, MaxLongitude);

    RuleFor(x => x.Latitude)
      .NotEmpty()
      .InclusiveBetween(MinLatitude, MaxLatitude);

    RuleFor(x => x.BriefDescription)
      .MaximumLength(ShortTextMaxLength);

    RuleFor(x => x.Description)
      .MaximumLength(TextMaxLength);

    RuleFor(x => x.PhoneNumber)
      .NotEmpty()
      .PhoneNumber();
  }
}