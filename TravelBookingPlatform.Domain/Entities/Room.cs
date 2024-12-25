namespace TravelBookingPlatform.Domain.Entities;

public class Room 
{
  public Guid RoomId { get; set; }
  public Guid RoomCategoryId { get; set; }
  public RoomCategory RoomCategory { get; set; }
  public string Number { get; set; }
  public ICollection<Booking> Bookings { get; set; } = new List<Booking>();
  public ICollection<InvoiceRecord> InvoiceRecords { get; set; } = new List<InvoiceRecord>();
  public DateTime CreatedAtUtc { get; set; }
  public DateTime? ModifiedAtUtc { get; set; }
}