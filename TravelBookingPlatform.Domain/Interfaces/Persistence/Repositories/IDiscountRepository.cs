using System.Linq.Expressions;
using TravelBookingPlatform.Domain.Entities;
using TravelBookingPlatform.Domain.Model;

namespace TravelBookingPlatform.Domain.Interfaces.Persistence.Repositories;

public interface IDiscountRepository
{
  Task<bool> ExistsAsync(Expression<Func<Discount, bool>> predicate,
                         CancellationToken cancellationToken = default);
  Task<Discount?> GetByIdAsync(Guid roomClassId, Guid id, CancellationToken cancellationToken);

  Task<Discount> CreateAsync(Discount discount, CancellationToken cancellationToken = default);
  
  Task DeleteAsync(Guid id, CancellationToken cancellationToken = default);

  Task<PaginatedResult<Discount>> GetAsync(EntityQuery<Discount> query,
    CancellationToken cancellationToken = default);
}