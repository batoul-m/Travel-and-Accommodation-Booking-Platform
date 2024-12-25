using System.Linq.Expressions;
using TravelBookingPlatform.Domain.Entities;
using TravelBookingPlatform.Domain.Model;

namespace TravelBookingPlatform.Domain.Interfaces.Persistence.Repositories;

public interface IReviewRepository
{
  Task<bool> ExistsAsync(Expression<Func<Review, bool>> predicate,
                         CancellationToken cancellationToken = default); 
  public Task<PaginatedResult<Review>> GetAsync(EntityQuery<Review> query,
    CancellationToken cancellationToken = default);

  Task<Review?> GetByIdAsync(Guid hotelId, Guid reviewId, CancellationToken cancellationToken = default);
  
  Task<Review> CreateAsync(Review review, CancellationToken cancellationToken = default);
  
  Task<Review?> GetByIdAsync(Guid reviewId, Guid hotelId, Guid guestId, CancellationToken cancellationToken = default);
 
  Task DeleteAsync(Guid reviewId, CancellationToken cancellationToken = default);
  
  Task<int> GetTotalRatingForHotelAsync(Guid hotelId, CancellationToken cancellationToken = default);
  
  Task<int> GetReviewCountForHotelAsync(Guid hotelId, CancellationToken cancellationToken = default);
  
  Task UpdateAsync(Review review, CancellationToken cancellationToken = default);
}