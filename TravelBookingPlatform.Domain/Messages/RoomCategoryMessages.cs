namespace TravelBookingPlatform.Domain.Messages;

public static class RoomCategoryMessages
{
    public const string NotFoundById = "The room class with the specified ID could not be found.";
    public const string DuplicateNameInHotel = "A room class with the same name already exists in the specified hotel.";
    public const string DuplicateRoomNumber = "A room with the same number already exists in the specified room class.";
    public const string RoomDoesNotExist = "The specified room does not exist in the specified room class.";
    public const string CannotDeleteWithDependencies = "There are existing rooms for the specified room class.";
}