using MediatR;

namespace TravelBookingPlatform.Application.Discounts.Delete;

public class DeleteDiscountCommand : IRequest
{
  public Guid RoomCategoryId { get; init; }
  public Guid DiscountId { get; init; }
}