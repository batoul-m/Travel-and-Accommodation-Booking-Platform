using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TravelBookingPlatform.Domain.Entities;

namespace TravelBookingPlatform.Infrastructure.Persistence.Configurations;

public class DiscountConfiguration : IEntityTypeConfiguration<Discount>
{
  public void Configure(EntityTypeBuilder<Discount> builder)
  {
    builder.HasKey(d => d.Id);
    
    builder.Property(d => d.Percentage)
      .HasPrecision(18, 2);
    
    builder.HasIndex(d => new { d.StartDateUtc, d.EndDateUtc });
  }
}