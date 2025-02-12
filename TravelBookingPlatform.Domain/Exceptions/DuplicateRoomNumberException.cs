namespace TravelBookingPlatform.Domain.Exceptions;

public class DuplicateRoomNumberException : Exception
{
    public DuplicateRoomNumberException(string message) : base(message) { }
}
