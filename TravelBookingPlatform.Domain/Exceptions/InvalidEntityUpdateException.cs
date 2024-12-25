namespace TravelBookingPlatform.Domain.Exceptions;

public class InvalidEntityUpdateException : Exception
{
    public InvalidEntityUpdateException() : base("The provided data for updating the entity is invalid.") { }
}
