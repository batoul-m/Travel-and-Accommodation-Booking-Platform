using System.Linq.Expressions;
using AutoMapper;
using MediatR;
using TravelBookingPlatform.Application.Extensions;
using TravelBookingPlatform.Domain.Entities;
using TravelBookingPlatform.Domain.Enums;
using TravelBookingPlatform.Domain.Interfaces.Persistence.Repositories;
using TravelBookingPlatform.Domain.Model;

namespace TravelBookingPlatform.Application.Hotels.Search;

public class SearchForHotelsHandler : IRequestHandler<SearchForHotelsQuery, PaginatedResult<HotelSearchResultResponse>>
{
  private readonly IHotelRepository _hotelRepository;
  private readonly IMapper _mapper;

  public SearchForHotelsHandler(
    IHotelRepository hotelRepository,
    IMapper mapper)
  {
    _hotelRepository = hotelRepository;
    _mapper = mapper;
  }

  public async Task<PaginatedResult<HotelSearchResultResponse>> Handle(
    SearchForHotelsQuery request,
    CancellationToken cancellationToken)
  {
    var searchResults = await _hotelRepository.GetForSearchAsync(
      new EntityQuery<Hotel>(
        BuildSearchExpression(request),
        request.SortOrder ?? SortOrder.Ascending,
        request.SortColumn,
        request.PageNumber,
        request.PageSize),
      cancellationToken);

    return _mapper.Map<PaginatedResult<HotelSearchResultResponse>>(searchResults);
  }

  private static Expression<Func<Hotel, bool>> BuildSearchExpression(SearchForHotelsQuery request)
  {
    return CreateSearchTermExpression(request)
      .And(CreateRoomTypeExpression(request))
      .And(CreateCapacityExpression(request))
      .And(CreatePriceRangeExpression(request))
      .And(CreateAmenitiesExpression(request))
      .And(CreateAvailableRoomsExpression(request))
      .And(CreateMinStarRatingExpression(request));
  }

  private static Expression<Func<Hotel, bool>> CreateSearchTermExpression(SearchForHotelsQuery request)
  {
    if (string.IsNullOrWhiteSpace(request.SearchTerm))
    {
      return _ => true;
    }

    return h => h.Name.Contains(request.SearchTerm) ||
                h.City.Name.Contains(request.SearchTerm) ||
                h.City.Country.Contains(request.SearchTerm);
  }

  private static Expression<Func<Hotel, bool>> CreateRoomTypeExpression(
    SearchForHotelsQuery request)
  {
    if (request.RoomTypes.Any())
    {
      return h => h.RoomCategory.Any(rc => request.RoomTypes.Contains(rc.RoomType));
    }

    return _ => true;
  }

  private static Expression<Func<Hotel, bool>> CreateCapacityExpression(
    SearchForHotelsQuery request)
  {
    return h => h.RoomCategory.Any(rc =>
      rc.AdultsCapacity >= request.NumberOfAdults && rc.ChildrenCapacity >= request.NumberOfChildren);
  }

  private static Expression<Func<Hotel, bool>> CreatePriceRangeExpression(
    SearchForHotelsQuery request)
  {
    Expression<Func<Hotel, bool>> greaterThenMinPriceExpression =
      request.MinPrice.HasValue
        ? h => h.RoomCategory.Any(rc => rc.PricePerNight >= request.MinPrice)
        : _ => true;


    Expression<Func<Hotel, bool>> lessThenMinPriceExpression =
      request.MaxPrice.HasValue
        ? h => h.RoomCategory.Any(rc => rc.PricePerNight <= request.MaxPrice)
        : _ => true;

    return greaterThenMinPriceExpression
      .And(lessThenMinPriceExpression);
  }

  private static Expression<Func<Hotel, bool>> CreateAmenitiesExpression(
    SearchForHotelsQuery request)
  {
    if (request.Amenities.Any())
    {
      return h => request.Amenities.All(amenityId => h.RoomCategory.Any(rc => rc.Amenities.Any(a => a.Id == amenityId)));
    }

    return _ => true;
  }

  private static Expression<Func<Hotel, bool>> CreateAvailableRoomsExpression(
    SearchForHotelsQuery request)
  {
    return h => h.RoomCategory.Any(rc => rc.Rooms.Count(r =>
      !r.Bookings.Any(b => request.CheckOutDate <= b.CheckInDateUtc || request.CheckInDate >= b.CheckOutDateUtc)
    ) >= request.NumberOfRooms);
  }

  private static Expression<Func<Hotel, bool>> CreateMinStarRatingExpression(SearchForHotelsQuery request)
  {
    if (request.MinStarRating.HasValue)
    {
      return h => h.StarRating >= request.MinStarRating;
    }

    return _ => true;
  }
}