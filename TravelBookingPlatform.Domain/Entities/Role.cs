using System.Text;
using TravelBookingPlatform.Domain.Enums;

namespace TravelBookingPlatform.Domain.Entities;

public class Role 
{
    public Guid Id { get; set;}
    public UserRole UserRole { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public ICollection<User> Users { get; set; } = new List<User>();
}