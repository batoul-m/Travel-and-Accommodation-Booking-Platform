using FluentValidation;
using TravelBookingPlatform.Api.Dtos.Rooms;

namespace TravelBookingPlatform.Api.Validators.Rooms;

public class RoomCreationRequestValidator : AbstractValidator<RoomCreationRequest>
{
  public RoomCreationRequestValidator()
  {
    RuleFor(x => x.Number)
      .NotEmpty();
  }
}