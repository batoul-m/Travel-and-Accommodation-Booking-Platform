using TravelBookingPlatform.Domain.Enums;

namespace TravelBookingPlatform.Domain.Entities;

public class Image
{
    public Guid Id { get; set; }
    public Guid EntityId { get; set; }
    public string Format { get; set; }
    public ImageType Type { get; set; }
    public string Path { get; set; }
}