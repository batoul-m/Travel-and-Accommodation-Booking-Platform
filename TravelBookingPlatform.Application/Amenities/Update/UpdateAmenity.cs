using MediatR;

namespace TravelBookingPlatform.Application.Amenities;

public class UpdateAmenity : IRequest
{
  public Guid AmenityId { get; init; }
  public string Name { get; init; }

  public string? Description { get; init; }
}