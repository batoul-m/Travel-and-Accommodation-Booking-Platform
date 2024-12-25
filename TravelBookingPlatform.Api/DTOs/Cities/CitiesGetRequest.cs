using TravelBookingPlatform.Api.Dtos.Common;

namespace TravelBookingPlatform.Api.Dtos.Cities;

public class CitiesGetRequest : ResourcesQueryRequest
{
  public string? SearchTerm { get; init; }
}