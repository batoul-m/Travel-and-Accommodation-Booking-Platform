namespace TravelBookingPlatform.Domain.Model;

public record PaginatedResult<TItem>(
    IEnumerable<TItem> Items,
    PaginationMetadata PaginationMetadata);
