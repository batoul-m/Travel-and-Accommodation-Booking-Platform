namespace TravelBookingPlatform.Domain.Exceptions;

public class GuestHasNoHotelBookingException : Exception
{
    public GuestHasNoHotelBookingException(string message) : base(message) { }
}
