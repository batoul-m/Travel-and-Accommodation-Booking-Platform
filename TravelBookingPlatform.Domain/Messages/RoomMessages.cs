namespace TravelBookingPlatform.Domain.Messages;

public static class RoomMessages
{
    public const string NotFoundById = "The room with the specified ID could not be found.";
    public const string NotInSameHotel = "The provided rooms are not in the same hotel.";
    public const string CannotDeleteWithBookings = "The specified room cannot be deleted because there are existing bookings.";

    public static string NotAvailable(Guid roomId)
    {
        return $"Room with ID {roomId} is not available during the specified time interval.";
    }
}