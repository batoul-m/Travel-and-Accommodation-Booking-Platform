using System.Linq.Expressions;
using TravelBookingPlatform.Domain.Entities;
using TravelBookingPlatform.Domain.Model;

namespace TravelBookingPlatform.Domain.Interfaces.Persistence.Repositories;

public interface IOwnerRepository
{
  Task<bool> ExistsAsync(Expression<Func<Owner, bool>> predicate,
                         CancellationToken cancellationToken = default);
  
  Task<PaginatedResult<Owner>> GetAsync(EntityQuery<Owner> query, CancellationToken cancellationToken = default);

  Task<Owner?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

  Task<Owner> CreateAsync(Owner owner, CancellationToken cancellationToken = default);

  Task UpdateAsync(Owner owner, CancellationToken cancellationToken = default);
}