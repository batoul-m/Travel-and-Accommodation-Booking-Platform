using MediatR;

namespace TravelBookingPlatform.Application.Discounts.GetById;

public class GetDiscountByIdQuery : IRequest<DiscountResponse>
{
  public Guid RoomClassId { get; init; }
  public Guid DiscountId { get; init; }
}