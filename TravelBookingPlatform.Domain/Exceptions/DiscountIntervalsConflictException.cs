namespace TravelBookingPlatform.Domain.Exceptions;

public class DiscountIntervalsConflictException : Exception
{
    public DiscountIntervalsConflictException(string message) : base(message) { }
}