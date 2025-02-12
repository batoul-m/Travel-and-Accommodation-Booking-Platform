using FluentValidation;
using TravelBookingPlatform.Api.Dtos.Images;
using TravelBookingPlatform.Application.Extensions.Validation;

namespace TravelBookingPlatform.Api.Validators.Images;

public class ImageCreationRequestValidator : AbstractValidator<ImageCreationRequest>
{
  public ImageCreationRequestValidator()
  {
    RuleFor(x => x.Image)
      .NotEmpty()
      .ValidImage();
  }
}