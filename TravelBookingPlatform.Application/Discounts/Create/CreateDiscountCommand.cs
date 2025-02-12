using MediatR;
using TravelBookingPlatform.Application.Discounts.GetById;

namespace TravelBookingPlatform.Application.Discounts.Create;

public class CreateDiscountCommand : IRequest<DiscountResponse>
{
  public Guid RoomClassId { get; init; }
  public decimal Percentage { get; init; }
  public DateTime StartDateUtc { get; init; }
  public DateTime EndDateUtc { get; init; }
}