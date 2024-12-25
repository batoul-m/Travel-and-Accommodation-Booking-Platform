namespace TravelBookingPlatform.Domain.Model;

public record Attachment(
    string Name,
    byte[] File,
    string MediaType,
    string SubMediaType);