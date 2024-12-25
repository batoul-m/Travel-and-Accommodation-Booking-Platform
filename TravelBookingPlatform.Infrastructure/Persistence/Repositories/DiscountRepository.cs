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

public class DiscountRepository : IDiscountRepository
{
    private readonly HotelBookingDbContext _context;

    public DiscountRepository(HotelBookingDbContext context)
    {
        _context = context;
    }

    public async Task<bool> ExistsAsync(Expression<Func<Discount, bool>> predicate, CancellationToken cancellationToken = default)
    {
        return await _context.Discounts.AnyAsync(predicate, cancellationToken);
    }

    public Task<Discount?> GetByIdAsync(
        Guid roomClassId, Guid id,
        CancellationToken cancellationToken = default)
    {
        return _context.Discounts
            .FirstOrDefaultAsync(d => d.Id == id && d.RoomClassId == roomClassId, cancellationToken);
    }

    public async Task<Discount> CreateAsync(Discount discount, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(discount);    
        var createdDiscount = await _context.Discounts.AddAsync(discount, cancellationToken);
        return createdDiscount.Entity;
    }

    public async Task DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        if (!await _context.Discounts.AnyAsync(r => r.Id == id, cancellationToken))
        {
            throw new NotFoundException(DiscountMessages.NotFoundById);
        }
        
        var entity = _context.ChangeTracker.Entries<Discount>()
                        .FirstOrDefault(e => e.Entity.Id == id)?.Entity
                      ?? new Discount { Id = id };

        _context.Discounts.Remove(entity);
    }

    public async Task<PaginatedResult<Discount>> GetAsync(
        EntityQuery<Discount> query,
        CancellationToken cancellationToken = default)
    {
        var queryable = _context.Discounts
            .Where(query.Filter)
            .SortByExpression(SortingExpressions.GetDiscountSortExpression(query.SortColumn), query.SortOrder);

        var itemsToReturn = await queryable
            .GetPage(query.PageNumber, query.PageSize)
            .AsNoTracking()
            .ToListAsync(cancellationToken);

        return new PaginatedResult<Discount>(
            itemsToReturn,
            await queryable.GetPaginationMetadataAsync(
                query.PageNumber,
                query.PageSize));
    }
}