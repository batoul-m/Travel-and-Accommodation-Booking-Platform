namespace TravelBookingPlatform.Domain.Exceptions;

public class DuplicateHotelLocationException : Exception
{
    public DuplicateHotelLocationException(string message) : base(message) { }
}
