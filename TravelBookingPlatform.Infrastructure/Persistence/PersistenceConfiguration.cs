using LinqToDB.EntityFrameworkCore;
using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TravelBookingPlatform.Domain.Interfaces.Persistence;
using TravelBookingPlatform.Domain.Interfaces.Persistence.Repositories;
using TravelBookingPlatform.Infrastructure.Persistence.DbContexts;
using TravelBookingPlatform.Infrastructure.Persistence.Repositories;
using TravelBookingPlatform.Infrastructure.Persistence.Services.Images;

namespace TravelBookingPlatform.Infrastructure.Persistence;

public static class PersistenceConfiguration
{
  internal static IServiceCollection AddPersistenceInfrastructure(
    this IServiceCollection services, 
    IConfiguration configuration)
  {
    services.AddDbContext(configuration)
      .AddPasswordHashing()
      .AddRepositories()
      .AddImageService();
    
    return services;
  }

  private static IServiceCollection AddDbContext(
    this IServiceCollection services, IConfiguration configuration)
  {
    services.AddDbContext<HotelBookingDbContext>(options =>
    {
      LinqToDBForEFTools.Initialize();
      
      options.UseSqlServer(configuration.GetConnectionString("SqlServer"),
          optionsBuilder => optionsBuilder.EnableRetryOnFailure(5))
        .UseLinqToDB();
    });

    services.AddScoped<IUnitOfWork, UnitOfWork>();
    
    return services;
  }

  private static IServiceCollection AddPasswordHashing(this IServiceCollection services)
  {
    services.AddScoped<IPasswordHasher, PasswordHasher>();

    return services;
  }

  private static IServiceCollection AddRepositories(this IServiceCollection services)
  {
    services.AddScoped<IAmenityRepository, AmenityRepository>()
      .AddScoped<IBookingRepository, BookingRepository>()
      .AddScoped<ICityRepository, CityRepository>()
      .AddScoped<IDiscountRepository, DiscountRepository>()
      .AddScoped<IHotelRepository, HotelRepository>()
      .AddScoped<IImageRepository, ImageRepository>()
      .AddScoped<IOwnerRepository, OwnerRepository>()
      .AddScoped<IRoleRepository, RoleRepository>()
      .AddScoped<IRoomClassRepository, RoomClassRepository>()
      .AddScoped<IRoomRepository, RoomRepository>()
      .AddScoped<IUserRepository, UserRepository>()
      .AddScoped<IReviewRepository, ReviewRepository>();

    return services;
  }
  
  public static IApplicationBuilder Migrate(this IApplicationBuilder app)
  {
    using var scope = app.ApplicationServices.CreateScope();
    
    using var context = scope.ServiceProvider.GetRequiredService<HotelBookingDbContext>();
    
    if (context.Database.GetPendingMigrations().Any()) {
      context.Database.Migrate();
    }
    
    return app;
  }
}