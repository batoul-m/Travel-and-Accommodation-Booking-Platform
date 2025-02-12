using TravelBookingPlatform.Api.Dtos.Common;

namespace TravelBookingPlatform.Api.Dtos.RoomClasses;

public class RoomClassesGetRequest : ResourcesQueryRequest
{
  public string? SearchTerm { get; init; }
}