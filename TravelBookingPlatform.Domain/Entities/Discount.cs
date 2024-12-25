namespace TravelBookingPlatform.Domain.Entities;

public class Discount
{
  public Guid Id { get; set; }
  public Guid RoomClassId { get; set; }
  public RoomCategory RoomCategory { get; set; }
  public decimal Percentage { get; set; }
  public DateTime StartDateUtc { get; set; }
  public DateTime EndDateUtc { get; set; }
  public DateTime CreatedAtUtc { get; set; }
}