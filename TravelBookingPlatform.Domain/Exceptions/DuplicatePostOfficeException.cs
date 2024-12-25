namespace TravelBookingPlatform.Domain.Exceptions;

public class DuplicatePostOfficeException : Exception
{
    public DuplicatePostOfficeException(string message) : base(message) { }
}