namespace TravelBookingPlatform.Domain.Exceptions;

public class DependentsExistException : Exception
{
    public DependentsExistException(string message) : base(message) { }
}