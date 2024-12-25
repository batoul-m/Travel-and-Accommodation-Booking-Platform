using TravelBookingPlatform.Api.Dtos.Common;

namespace TravelBookingPlatform.Api.Dtos.Amenities;

public class AmenitiesGetRequest : ResourcesQueryRequest
{
  public string? SearchTerm { get; init; }
}