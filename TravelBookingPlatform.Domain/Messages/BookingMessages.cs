namespace TravelBookingPlatform.Domain.Messages;

public static class BookingMessages
{
    public const string NotFoundById = "The booking with the specified ID could not be found.";
    public const string CannotCancelAfterCheckIn = "The booking cannot be canceled because the check-in date has passed.";
    public const string NotFoundForSpecifiedGuest = "The booking with the specified ID could not be found for the given guest.";
    public const string NoBookingInHotelForGuest = "The specified guest has not made any bookings at the specified hotel.";
}