using MediatR;
using TravelBookingPlatform.Domain.Enums;
using TravelBookingPlatform.Domain.Model;

namespace TravelBookingPlatform.Application.Hotels.Search;

public class SearchForHotelsQuery : IRequest<PaginatedResult<HotelSearchResultResponse>>
{
  public string? SearchTerm { get; init; }
  public SortOrder? SortOrder { get; init; }
  public string? SortColumn { get; init; }
  public int PageNumber { get; init; }
  public int PageSize { get; init; }
  public DateOnly CheckInDate { get; init; }
  public DateOnly CheckOutDate { get; init; }
  public int NumberOfAdults { get; init; }
  public int NumberOfChildren { get; init; }
  public int NumberOfRooms { get; init; }
  public decimal? MinPrice { get; init; }
  public decimal? MaxPrice { get; init; }
  public int? MinStarRating { get; init; }
  public IEnumerable<RoomType> RoomTypes { get; init; }
  public IEnumerable<Guid> Amenities { get; init; }
}