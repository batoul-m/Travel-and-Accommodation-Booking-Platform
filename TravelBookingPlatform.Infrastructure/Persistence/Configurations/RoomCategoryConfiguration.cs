using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TravelBookingPlatform.Domain.Entities;
using TravelBookingPlatform.Domain.Enums;

namespace TravelBookingPlatform.Infrastructure.Persistence.Configurations;

public class RoomCategoryConfiguration : IEntityTypeConfiguration<RoomCategory>
{
  public void Configure(EntityTypeBuilder<RoomCategory> builder)
  {
    builder.HasKey(rc => rc.Id);

    builder.Property(rc => rc.RoomType)
      .HasConversion(new EnumToStringConverter<RoomType>());
    
    builder.Ignore(h => h.Gallery);

    builder.HasMany(rc => rc.Rooms)
      .WithOne(r => r.RoomCategory)
      .IsRequired()
      .OnDelete(DeleteBehavior.Restrict);

    builder.HasMany(rc => rc.Amenities)
      .WithMany(a => a.RoomsCategory);
    
    builder.Property(rc => rc.PricePerNight)
      .HasPrecision(18, 2);
    
    builder.HasIndex(rc => rc.RoomType);

    builder.HasIndex(rc => new { rc.AdultsCapacity, rc.ChildrenCapacity });

    builder.HasIndex(rc => rc.PricePerNight);
  }
}