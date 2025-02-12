namespace TravelBookingPlatform.Domain.Exceptions;

public class RoomUnavailableException : Exception
{
    public RoomUnavailableException(string message) : base(message) { }
}
