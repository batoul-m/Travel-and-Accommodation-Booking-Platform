using TravelBookingPlatform.Domain.Entities;

namespace TravelBookingPlatform.Domain.Interfaces.Persistence.Repositories;

public interface IRoleRepository
{
  Task<Role?> GetByNameAsync(string name, CancellationToken cancellationToken = default);
}