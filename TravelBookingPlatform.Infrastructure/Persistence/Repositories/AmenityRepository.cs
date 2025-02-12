using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using TravelBookingPlatform.Infrastructure.Persistence.Extensions;
using TravelBookingPlatform.Domain.Entities;
using TravelBookingPlatform.Domain.Exceptions;
using TravelBookingPlatform.Domain.Interfaces.Persistence.Repositories;
using TravelBookingPlatform.Domain.Messages;
using TravelBookingPlatform.Domain.Model;
using TravelBookingPlatform.Infrastructure.Persistence.DbContexts;
using TravelBookingPlatform.Infrastructure.Persistence.Helpers;

namespace TravelBookingPlatform.Infrastructure.Persistence.Repositories;

public class AmenityRepository : IAmenityRepository
{
    private readonly HotelBookingDbContext _context;

    public AmenityRepository(HotelBookingDbContext context)
    {
        _context = context;
    }

    public async Task<Amenity?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _context.Amenities.FindAsync(new object[] { id }, cancellationToken);
    }

    public async Task<bool> ExistsAsync(Expression<Func<Amenity, bool>> predicate, CancellationToken cancellationToken = default)
    {
        return await _context.Amenities.AnyAsync(predicate, cancellationToken);
    }

    public async Task<Amenity> CreateAsync(Amenity amenity, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(amenity);
        var createdAmenity = await _context.Amenities
            .AddAsync(amenity, cancellationToken);

        return createdAmenity.Entity;
    }

    public async Task UpdateAsync(Amenity amenity, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(amenity);
        if (!await _context.Amenities.AnyAsync(a => a.Id == amenity.Id, cancellationToken))
        {
            throw new NotFoundException(AmenityMessages.NotFoundById);
        }
        _context.Amenities.Update(amenity);
    }

    public async Task<PaginatedResult<Amenity>> GetAsync(EntityQuery<Amenity> query, CancellationToken cancellationToken = default)
    {
        var queryable = _context.Amenities
            .Where(query.Filter)
            .SortByExpression(SortingExpressions.GetAmenitySortExpression(query.SortColumn), query.SortOrder);
        
        var itemsToReturn = await queryable
            .GetPage(query.PageNumber, query.PageSize)
            .AsNoTracking()
            .ToListAsync(cancellationToken);

        var metadata = await queryable.GetPaginationMetadataAsync(
            query.PageNumber,
            query.PageSize);

        return new PaginatedResult<Amenity>(itemsToReturn, metadata);
    }
}