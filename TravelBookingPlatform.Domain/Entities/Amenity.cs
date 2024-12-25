namespace TravelBookingPlatform.Domain.Entities;

public class Amenity
{
    public Guid Id { get; set; } 
    public string Name { get; set; } 
    public string? Description { get; set; } 
    public bool IsAvailable { get; set; } 
    public ICollection<RoomCategory> RoomsCategory { get; set; } = new List<RoomCategory>();
}