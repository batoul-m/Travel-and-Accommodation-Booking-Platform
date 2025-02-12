using System.Linq.Expressions;
using TravelBookingPlatform.Domain.Entities;
using TravelBookingPlatform.Domain.Model;

namespace TravelBookingPlatform.Domain.Interfaces.Persistence.Repositories;

public interface IRoomClassRepository
{
  Task<bool> ExistsAsync(Expression<Func<RoomCategory, bool>> predicate,
                         CancellationToken cancellationToken = default);
  Task<RoomCategory?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

  Task<RoomCategory> CreateAsync(RoomCategory roomCategory, CancellationToken cancellationToken = default);

  Task UpdateAsync(RoomCategory roomClass, CancellationToken cancellationToken = default);

  Task DeleteAsync(Guid id, CancellationToken cancellationToken = default);

  Task<PaginatedResult<RoomCategory>> GetAsync(
    EntityQuery<RoomCategory> query,
    bool includeGallery = false,
    CancellationToken cancellationToken = default);

  Task<IEnumerable<RoomCategory>> GetFeaturedDealsInDifferentHotelsAsync(int count,
    CancellationToken cancellationToken = default);
}