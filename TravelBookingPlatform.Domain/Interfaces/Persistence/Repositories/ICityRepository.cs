using System.Linq.Expressions;
using TravelBookingPlatform.Domain.Entities;
using TravelBookingPlatform.Domain.Model;

namespace TravelBookingPlatform.Domain.Interfaces.Persistence.Repositories;

public interface ICityRepository
{
  Task<bool> ExistsAsync(Expression<Func<City, bool>> predicate,
                         CancellationToken cancellationToken = default);
  Task<PaginatedResult<CityInformations>> GetForManagementAsync(EntityQuery<City> query,
    CancellationToken cancellationToken = default);

  Task<City?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

  Task<City> CreateAsync(City city, CancellationToken cancellationToken = default);

  Task DeleteAsync(Guid id, CancellationToken cancellationToken = default);

  Task UpdateAsync(City city, CancellationToken cancellationToken = default);

  Task<IEnumerable<City>> GetMostVisitedAsync(int count, CancellationToken cancellationToken = default);
}