using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TravelBookingPlatform.Domain.Entities;

namespace TravelBookingPlatform.Infrastructure.Persistence.Configurations;

public class AmenityConfiguration : IEntityTypeConfiguration<Amenity>
{
  public void Configure(EntityTypeBuilder<Amenity> builder)
  {
    builder.HasKey(a => a.Id);

    builder.HasMany(a => a.RoomsCategory)
      .WithMany(rc => rc.Amenities);
  }
}