namespace TravelBookingPlatform.Domain.Messages;

public static class DiscountMessages
{
    public const string NotFoundById = "The discount with the specified ID could not be found.";
    public const string NotFoundInRoomClass = "The discount with the specified ID could not be found in the specified room class.";
    public const string OverlappingDateInterval = "Another discount already exists within the same date interval.";
}