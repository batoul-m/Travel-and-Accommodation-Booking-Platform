namespace TravelBookingPlatform.Domain.Exceptions;

public class PaymentProcessingException : Exception
{
    public PaymentProcessingException() : base("An error occurred while processing the payment.") { }
}
