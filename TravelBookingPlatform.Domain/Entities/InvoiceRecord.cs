namespace TravelBookingPlatform.Domain.Entities;

public class InvoiceRecord
{
  public Guid Id { get; set; }
  public Guid BookingId { get; set; }
  public Booking Booking { get; set; }
  public Guid RoomId { get; set; }
  public string RoomCategoryName { get; set; }
  public string RoomNumber { get; set; }
  public decimal PriceAtBooking { get; set; }
  public decimal? DiscountPercentageAtBooking { get; set; }
}