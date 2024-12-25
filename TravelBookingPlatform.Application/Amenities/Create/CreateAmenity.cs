using MediatR;
using TravelBookingPlatform.Application.Amenities;

namespace TravelBookingPlatform.Application.Amenities.Create;

public class CreateAmenity : IRequest<AmenityResponse>
{
  public string Name { get; init; }
  public string? Description { get; init; }
}