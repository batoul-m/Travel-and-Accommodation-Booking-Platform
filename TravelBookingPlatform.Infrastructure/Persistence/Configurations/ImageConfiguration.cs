using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TravelBookingPlatform.Domain.Entities;
using TravelBookingPlatform.Domain.Enums;

namespace TravelBookingPlatform.Infrastructure.Persistence.Configurations;

public class ImageConfiguration : IEntityTypeConfiguration<Image>
{
  public void Configure(EntityTypeBuilder<Image> builder)
  {
    builder.HasKey(i => i.Id);

    builder.Property(i => i.Type)
      .HasConversion(new EnumToStringConverter<ImageType>());
    
    builder.HasIndex(i => i.EntityId);
  }
}