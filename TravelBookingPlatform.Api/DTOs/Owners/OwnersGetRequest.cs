using TravelBookingPlatform.Api.Dtos.Common;

namespace TravelBookingPlatform.Api.Dtos.Owners;

public class OwnersGetRequest : ResourcesQueryRequest
{
  public string? SearchTerm { get; init; }
}