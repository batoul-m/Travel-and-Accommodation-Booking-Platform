using MediatR;
using TravelBookingPlatform.Application.Owners.Common;

namespace TravelBookingPlatform.Application.Owners.Create;

public class CreateOwnerCommand : IRequest<OwnerResponse>
{
  public string FirstName { get; init; }
  public string LastName { get; init; }
  public string Email { get; init; }
  public string PhoneNumber { get; init; }
}