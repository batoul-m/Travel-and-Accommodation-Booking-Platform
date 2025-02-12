using TravelBookingPlatform.Domain.Entities;

namespace TravelBookingPlatform.Domain.Model;

public class HotelInformations
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public int StarRating { get; set; }
    public Owner Owner { get; set; }
    public int NumberOfRooms { get; set; }
    public DateTime CreatedAtUtc { get; set; }
    public DateTime? ModifiedAtUtc { get; set; }
}