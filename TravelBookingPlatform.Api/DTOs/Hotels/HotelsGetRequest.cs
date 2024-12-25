using TravelBookingPlatform.Api.Dtos.Common;

namespace TravelBookingPlatform.Api.Dtos.Hotels;

public class HotelsGetRequest : ResourcesQueryRequest
{
  public string? SearchTerm { get; init; }
}