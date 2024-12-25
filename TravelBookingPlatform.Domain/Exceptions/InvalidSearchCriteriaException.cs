namespace TravelBookingPlatform.Domain.Exceptions;

public class InvalidSearchCriteriaException : Exception
{
    public InvalidSearchCriteriaException() : base("The search criteria provided are invalid.") { }
}
