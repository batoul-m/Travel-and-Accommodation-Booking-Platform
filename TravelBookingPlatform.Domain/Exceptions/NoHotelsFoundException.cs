namespace TravelBookingPlatform.Domain.Exceptions;

public class NoHotelsFoundException : Exception
{
    public NoHotelsFoundException() : base("No hotels match the search criteria.") { }
}
