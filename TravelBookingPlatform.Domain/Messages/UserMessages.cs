namespace TravelBookingPlatform.Domain.Messages;

public static class UserMessages
{
    public const string NotFoundById = "The user with the specified ID could not be found.";
    public const string EmailAlreadyExists = "A user with the provided email already exists.";
    public const string InvalidCredentials = "The provided credentials are not valid.";
    public const string InvalidRole = "The provided role is invalid.";
    public const string NotAuthenticated = "User is not authenticated.";
    public const string NotAGuest = "The authenticated user is not a guest.";
}