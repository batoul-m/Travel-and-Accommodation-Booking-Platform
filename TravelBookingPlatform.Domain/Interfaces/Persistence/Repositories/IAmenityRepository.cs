using System.Linq.Expressions;
using TravelBookingPlatform.Domain.Entities;
using TravelBookingPlatform.Domain.Model;

namespace TravelBookingPlatform.Domain.Interfaces.Persistence.Repositories;

public interface IAmenityRepository
{
  Task<PaginatedResult<Amenity>> GetAsync(EntityQuery<Amenity> query, CancellationToken cancellationToken = default);

  Task<Amenity?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

  Task<bool> ExistsAsync(Expression<Func<Amenity, bool>> predicate, CancellationToken cancellationToken = default);

  Task<Amenity> CreateAsync(Amenity amenity, CancellationToken cancellationToken = default);

  Task UpdateAsync(Amenity amenity, CancellationToken cancellationToken = default);
}