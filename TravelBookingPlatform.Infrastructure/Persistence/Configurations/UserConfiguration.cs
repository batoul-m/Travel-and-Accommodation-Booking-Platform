using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TravelBookingPlatform.Domain.Entities;

namespace TravelBookingPlatform.Infrastructure.Persistence.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
  public void Configure(EntityTypeBuilder<User> builder)
  {
    builder.HasKey(u => u.Id);

    builder.HasIndex(u => u.Email).IsUnique();
    
    builder.HasData([
      new User
      {
        Id = new Guid("b0a3e9b6-2b47-4fbd-9a9d-5c7c07fbcf45"),
        FirstName = "Admin",
        LastName = "-",
        Email = "admin@travelbooking.com",
        Password = "$2a$12$hdh7RHkZG9H1NkNJpoXs1upXbD9ug3LBhs0Ok7Eq2XP7GrxLwzJ3a" // hashed password.
      },
      new User
      {
        Id = new Guid("ce4b3194-6d9f-4b72-a15d-42b505a9b4ad"),
        FirstName = "manager",
        LastName = "-",
        Email = "manager@travelbooking.com",
        Password = "$2a$12$7cQg8.ZpsjO75AqKScINhux2QX50sL9VYXvB3oMM2Bh.1TV2Aq3NO" // hashed password.
      },
    ]);
    
    builder.HasMany(u => u.Roles)
      .WithMany(r => r.Users)
      .UsingEntity<Dictionary<string, object>>(
        "UserRole",
        j => j.HasOne<Role>().WithMany()
          .HasForeignKey("RoleId").OnDelete(DeleteBehavior.Cascade),
        j => j.HasOne<User>().WithMany()
          .HasForeignKey("UserId").OnDelete(DeleteBehavior.Cascade))
      .HasData([new Dictionary<string, object>{
        ["UserId"] = new Guid("ba867dc2-8104-4765-836d-bce1a98ebb03"), 
        ["RoleId"] = new Guid("d2e7d2bb-bc77-4a6d-a43a-763716c6df8b")
      }]);
  }
}