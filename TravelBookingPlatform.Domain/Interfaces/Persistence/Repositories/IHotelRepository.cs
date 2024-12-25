using System.Linq.Expressions;
using TravelBookingPlatform.Domain.Entities;
using TravelBookingPlatform.Domain.Model;

namespace TravelBookingPlatform.Domain.Interfaces.Persistence.Repositories;

public interface IHotelRepository
{
  Task<bool> ExistsAsync(Expression<Func<Hotel, bool>> predicate,
                         CancellationToken cancellationToken = default);
  
  Task<PaginatedResult<HotelInformations>> GetForManagementAsync(EntityQuery<Hotel> query,
    CancellationToken cancellationToken = default);

  Task<Hotel?> GetByIdAsync(Guid id, bool includeCity = false, bool includeThumbnail = false,
    bool includeGallery = false, CancellationToken cancellationToken = default);

  Task<Hotel> CreateAsync(Hotel hotel, CancellationToken cancellationToken = default);

  Task UpdateAsync(Hotel hotel, CancellationToken cancellationToken = default);

  Task DeleteAsync(Guid id, CancellationToken cancellationToken = default);

  Task<PaginatedResult<HotelSearchResult>> GetForSearchAsync(EntityQuery<Hotel> query,
    CancellationToken cancellationToken = default);

  Task UpdateReviewById(Guid id, double newRating, CancellationToken cancellationToken = default);
}