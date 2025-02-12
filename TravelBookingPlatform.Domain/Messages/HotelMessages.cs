namespace TravelBookingPlatform.Domain.Messages;

public static class HotelMessages
{
    public const string NotFoundById = "The hotel with the specified ID could not be found.";
    public const string DuplicateLocation = "A hotel with the same location (longitude, latitude) already exists.";
    public const string CannotDeleteWithDependencies = "The specified hotel cannot be deleted because it has dependent records.";
}

