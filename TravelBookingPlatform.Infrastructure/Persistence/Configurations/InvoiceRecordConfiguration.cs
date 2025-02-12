using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TravelBookingPlatform.Domain.Entities;

namespace TravelBookingPlatform.Infrastructure.Persistence.Configurations;

public class InvoiceRecordConfiguration : IEntityTypeConfiguration<InvoiceRecord>
{
  public void Configure(EntityTypeBuilder<InvoiceRecord> builder)
  {
    builder.HasKey(ir => ir.Id);
    
    builder.Property(ir => ir.PriceAtBooking)
      .HasPrecision(18, 2);

    builder.Property(ir => ir.DiscountPercentageAtBooking)
      .HasPrecision(18, 2);
  }
}