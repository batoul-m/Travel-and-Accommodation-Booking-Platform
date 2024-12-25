namespace TravelBookingPlatform.Domain.Messages;

public static class CityMessages
{
    public const string NotFoundById = "The city with the specified ID could not be found.";
    public const string DuplicatePostalCode = "A city with the provided postal code already exists.";
    public const string CannotDeleteWithDependencies = "The specified city cannot be deleted because it has dependent records.";
}