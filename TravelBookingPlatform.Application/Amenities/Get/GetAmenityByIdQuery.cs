using MediatR;

namespace TravelBookingPlatform.Application.Amenities;

public class GetAmenityByIdQuery : IRequest<AmenityResponse>
{
  public Guid AmenityId { get; init; }
}