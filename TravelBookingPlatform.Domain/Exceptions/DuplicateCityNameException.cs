namespace TravelBookingPlatform.Domain.Exceptions;

public class DuplicateCityNameException : Exception
{
    public DuplicateCityNameException(string message) : base(message) { }
}
