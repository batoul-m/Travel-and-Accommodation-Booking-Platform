using TravelBookingPlatform.Api.Dtos.Common;

namespace TravelBookingPlatform.Api.Dtos.Rooms;

public class RoomsForGuestsGetRequest : ResourcesQueryRequest
{
  public DateOnly CheckInDateUtc { get; init; }
  public DateOnly CheckOutDateUtc { get; init; }
}