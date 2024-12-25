namespace TravelBookingPlatform.Domain.Exceptions;

public class ResourceNotFoundException: Exception
{
    public ResourceNotFoundException() : base("Resource Not Found") { }
}