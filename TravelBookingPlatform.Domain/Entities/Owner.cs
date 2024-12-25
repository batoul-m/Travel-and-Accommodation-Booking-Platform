namespace TravelBookingPlatform.Domain.Entities;

public class Owner
{
    public Guid Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    public ICollection<Hotel> Hotels { get; set; } = new List<Hotel>();
}