namespace TravelBookingPlatform.Domain.Exceptions;

public class RoomDoesNotBelongToHotelException : Exception
{
    public RoomDoesNotBelongToHotelException(string message) : base(message) { }
}
