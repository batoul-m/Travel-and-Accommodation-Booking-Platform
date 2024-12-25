namespace TravelBookingPlatform.Domain.Exceptions;

public class AmenityExistsException : Exception
{
    public AmenityExistsException(string message) : base(message) { }
}