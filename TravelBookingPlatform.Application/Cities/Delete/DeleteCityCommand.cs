using MediatR;

namespace TravelBookingPlatform.Application.Cities.Delete;

public class DeleteCityCommand : IRequest
{
  public Guid CityId { get; init; }
}