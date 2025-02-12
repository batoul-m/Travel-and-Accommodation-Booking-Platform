using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace TravelBookingPlatform.Infrastructure.Persistence.DbContexts
{
    public class HotelBookingDbContextFactory : IDesignTimeDbContextFactory<HotelBookingDbContext>
    {
        public HotelBookingDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<HotelBookingDbContext>();
            optionsBuilder.UseSqlServer("Server=localhost;Database=TravelBookingPlatformServer;User Id=sa;Password=11aaAA@@;TrustServerCertificate=True;");  

            return new HotelBookingDbContext(optionsBuilder.Options);
        }
    }
}
