using FluentValidation;
using TravelBookingPlatform.Api.Dtos.Rooms;

namespace TravelBookingPlatform.Api.Validators.Rooms;

public class RoomUpdateRequestValidator : AbstractValidator<RoomUpdateRequest>
{
  public RoomUpdateRequestValidator()
  {
    RuleFor(x => x.Number)
      .NotEmpty();
  }
}