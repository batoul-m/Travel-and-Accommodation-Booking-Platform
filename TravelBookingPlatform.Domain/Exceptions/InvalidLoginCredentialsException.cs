namespace TravelBookingPlatform.Domain.Exceptions;

public class InvalidLoginCredentialsException : Exception
{
    public InvalidLoginCredentialsException() : base("The username or password provided is incorrect.") { }
}
