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

public class OwnerRepository : IOwnerRepository
{
    private readonly HotelBookingDbContext _context;

    public OwnerRepository(HotelBookingDbContext context)
    {
        _context = context;
    }

    public async Task<bool> ExistsAsync(Expression<Func<Owner, bool>> predicate, CancellationToken cancellationToken = default)
    {
        return await _context.Owners.AnyAsync(predicate, cancellationToken);
    }

    public async Task<PaginatedResult<Owner>> GetAsync(EntityQuery<Owner> query, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(query);

        var queryable = _context.Owners
            .Where(query.Filter)
            .SortByExpression(SortingExpressions.GetOwnerSortExpression(query.SortColumn), query.SortOrder);

        var itemsToReturn = await queryable
            .GetPage(query.PageNumber, query.PageSize)
            .AsNoTracking()
            .ToListAsync(cancellationToken);

        return new PaginatedResult<Owner>(
            itemsToReturn,
            await queryable.GetPaginationMetadataAsync(
                query.PageNumber,
                query.PageSize));
    }

    public async Task<Owner?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _context.Owners.FindAsync(new object[] { id }, cancellationToken);
    }

    public async Task<Owner> CreateAsync(Owner owner, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(owner);

        var addedEntity = await _context.AddAsync(owner, cancellationToken);
        return addedEntity.Entity;
    }

    public async Task UpdateAsync(Owner owner, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(owner);

        if (!await _context.Owners.AnyAsync(o => o.Id == owner.Id, cancellationToken))
        {
            throw new NotFoundException(OwnerMessages.NotFoundById);
        }

        _context.Owners.Update(owner);
    }
}