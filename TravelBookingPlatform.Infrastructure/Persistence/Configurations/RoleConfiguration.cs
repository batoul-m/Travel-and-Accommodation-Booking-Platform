using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TravelBookingPlatform.Domain.Entities;
using TravelBookingPlatform.Domain.Enums;

namespace TravelBookingPlatform.Infrastructure.Persistence.Configurations;

public class RoleConfiguration : IEntityTypeConfiguration<Role>
{
  public void Configure(EntityTypeBuilder<Role> builder)
  {
    builder.HasKey(r => r.Id);
    builder.HasMany(r => r.Users)
           .WithMany(u => u.Roles);

builder.HasData(
    new Role
    {
        Id = Guid.Parse("3d736b28-cf80-4dc1-8e49-453e3760f0be"),
        UserRole = UserRole.Guest,
        Name = "Guest",
        Description = "Guest role description"
    },
    new Role
    {
        Id = Guid.Parse("d2e7d2bb-bc77-4a6d-a43a-763716c6df8b"),
        UserRole = UserRole.Admin,
        Name = "Admin",
        Description = "Admin role description"
    },
    new Role
    {
        Id = Guid.Parse("b3e735b8-cf80-4dc1-8e49-453e3760f0be"),  // Ensure this is in the correct format
        UserRole = UserRole.Manager,
        Name = "Manager",
        Description = "Manager role description"
    }
);
  }
}
