namespace TravelBookingPlatform.Domain.Messages;

public static class ReviewMessages
{
    public const string NotFoundById = "The review with the specified ID could not be found.";
    public const string NotFoundInHotelById = "The review with the specified ID is not found for the hotel with the specified ID.";
    public const string NotFoundForUserAndHotel = "The specified review is not found for the specified user and hotel.";
    public const string GuestHasAlreadyReviewedHotel = "The specified guest has already reviewed the specified hotel.";
}