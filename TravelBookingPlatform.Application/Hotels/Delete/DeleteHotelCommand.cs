using MediatR;

namespace TravelBookingPlatform.Application.Hotels.Delete;

public class DeleteHotelCommand : IRequest
{
  public Guid HotelId { get; init; }
}