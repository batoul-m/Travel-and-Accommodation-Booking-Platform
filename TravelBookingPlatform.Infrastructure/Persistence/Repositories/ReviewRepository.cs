using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using TravelBookingPlatform.Domain.Entities;
using TravelBookingPlatform.Domain.Exceptions;
using TravelBookingPlatform.Domain.Interfaces.Persistence.Repositories;
using TravelBookingPlatform.Domain.Messages;
using TravelBookingPlatform.Domain.Model;
using TravelBookingPlatform.Infrastructure.Persistence.DbContexts;
using TravelBookingPlatform.Infrastructure.Persistence.Extensions;
using TravelBookingPlatform.Infrastructure.Persistence.Helpers;

namespace TravelBookingPlatform.Infrastructure.Persistence.Repositories;

public class ReviewRepository : IReviewRepository
{
    private readonly HotelBookingDbContext _context;

    public ReviewRepository(HotelBookingDbContext context)
    {
        _context = context;
    }

    public async Task<bool> ExistsAsync(Expression<Func<Review, bool>> predicate, CancellationToken cancellationToken = default)
    {
        return await _context.Reviews.AnyAsync(predicate, cancellationToken);
    }

    public async Task<PaginatedResult<Review>> GetAsync(EntityQuery<Review> query, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(query);

        var queryable = _context.Reviews
            .Where(query.Filter)
            .SortByExpression(SortingExpressions.GetReviewSortExpression(query.SortColumn), query.SortOrder);

        var itemsToReturn = await queryable
            .GetPage(query.PageNumber, query.PageSize)
            .AsNoTracking()
            .ToListAsync(cancellationToken);

        return new PaginatedResult<Review>(
            itemsToReturn,
            await queryable.GetPaginationMetadataAsync(
                query.PageNumber,
                query.PageSize));
    }

    public async Task<Review?> GetByIdAsync(Guid hotelId, Guid reviewId, CancellationToken cancellationToken = default)
    {
        return await _context.Reviews
            .FirstOrDefaultAsync(r => r.Id == reviewId && r.HotelId == hotelId, cancellationToken);
    }

    public async Task<Review> CreateAsync(Review review, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(review);

        var addedReview = await _context.Reviews.AddAsync(review, cancellationToken);
        return addedReview.Entity;
    }

    public async Task<Review?> GetByIdAsync(Guid reviewId, Guid hotelId, Guid guestId, CancellationToken cancellationToken = default)
    {
        return await _context.Reviews
            .FirstOrDefaultAsync(r => r.Id == reviewId && r.HotelId == hotelId && r.GuestId == guestId, cancellationToken);
    }

    public async Task DeleteAsync(Guid reviewId, CancellationToken cancellationToken = default)
    {
        if (!await _context.Reviews.AnyAsync(r => r.Id == reviewId, cancellationToken))
        {
            throw new NotFoundException(ReviewMessages.NotFoundById);
        }

        var entity = _context.ChangeTracker.Entries<Review>()
            .FirstOrDefault(e => e.Entity.Id == reviewId)?.Entity
            ?? new Review { Id = reviewId };

        _context.Reviews.Remove(entity);
    }

    public async Task<int> GetTotalRatingForHotelAsync(Guid hotelId, CancellationToken cancellationToken = default)
    {
        return await _context.Reviews
            .Where(r => r.HotelId == hotelId)
            .SumAsync(r => r.Rating, cancellationToken);
    }

    public async Task<int> GetReviewCountForHotelAsync(Guid hotelId, CancellationToken cancellationToken = default)
    {
        return await _context.Reviews
            .Where(r => r.HotelId == hotelId)
            .CountAsync(cancellationToken);
    }

    public async Task UpdateAsync(Review review, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(review);

        if (!await _context.Reviews.AnyAsync(r => r.Id == review.Id, cancellationToken))
        {
            throw new NotFoundException(ReviewMessages.NotFoundById);
        }

        _context.Reviews.Update(review);
    }
}