using MediatR;

namespace TravelBookingPlatform.Application.Cities.GetTrending;

public class GetTrendingCitiesQuery : IRequest<IEnumerable<TrendingCityResponse>>
{
  public int Count { get; init; }
}