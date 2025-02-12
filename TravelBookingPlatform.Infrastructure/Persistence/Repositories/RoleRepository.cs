using Microsoft.EntityFrameworkCore;
using TravelBookingPlatform.Domain.Entities;
using TravelBookingPlatform.Domain.Interfaces.Persistence.Repositories;
using TravelBookingPlatform.Infrastructure.Persistence.DbContexts;
using TravelBookingPlatform.Domain.Enums;

namespace TravelBookingPlatform.Infrastructure.Persistence.Repositories;

public class RoleRepository : IRoleRepository
{
    private readonly HotelBookingDbContext _context;

    public RoleRepository(HotelBookingDbContext context)
    {
        _context = context;
    }

    public async Task<Role?> GetByNameAsync(string name, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(name, nameof(name));
        
        return await _context.Roles
            .FirstOrDefaultAsync(r => r.Name == name, cancellationToken);
    }
}