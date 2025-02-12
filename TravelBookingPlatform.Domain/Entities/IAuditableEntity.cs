namespace TravelBookingPlatform.Domain.Entities;

public interface IAuditableEntity
{
    DateTime CreatedAtUtc { get; set; }
    DateTime? ModifiedAtUtc { get; set; }
}
