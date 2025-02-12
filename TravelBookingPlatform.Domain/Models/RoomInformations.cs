namespace TravelBookingPlatform.Domain.Model;

public class RoomInformations
{
    public Guid Id { get; set; }
    public Guid RoomClassId { get; set; }
    public string Number { get; set; }
    public bool IsAvailable { get; set; }
    public DateTime CreatedAtUtc { get; set; }
    public DateTime? ModifiedAtUtc { get; set; }
}