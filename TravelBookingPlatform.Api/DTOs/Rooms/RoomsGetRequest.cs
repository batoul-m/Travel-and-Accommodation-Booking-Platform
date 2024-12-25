using TravelBookingPlatform.Api.Dtos.Common;

namespace TravelBookingPlatform.Api.Dtos.Rooms;

public class RoomsGetRequest : ResourcesQueryRequest
{
  public string? SearchTerm { get; init; }
}