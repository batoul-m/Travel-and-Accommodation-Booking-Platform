﻿using FluentValidation;
using TravelBookingPlatform.Infrastructure.Common;

namespace TravelBookingPlatform.Infrastructure.Persistence.Services.Images;

public class FireBaseConfigValidator : AbstractValidator<FirebaseConfig>
{
  public FireBaseConfigValidator()
  {
    RuleFor(x => x.Bucket)
      .NotEmpty();

    RuleFor(x => x.CredentialsJson)
      .NotEmpty()
      .ValidJson();
  }
}