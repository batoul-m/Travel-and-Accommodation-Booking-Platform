using MediatR;
using TravelBookingPlatform.Application.Owners.Common;

namespace TravelBookingPlatform.Application.Owners.GetById;

public class GetOwnerByIdQuery : IRequest<OwnerResponse>
{
  public Guid OwnerId { get; init; }
}