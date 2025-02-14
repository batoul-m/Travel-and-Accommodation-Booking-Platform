namespace TravelBookingPlatform.Domain.Model;

public record PaginationMetadata(
    int TotalItemCount,
    int CurrentPage,
    int PageSize)
{
    public int TotalPageCount => (int)Math.Ceiling((double)TotalItemCount / PageSize);
}