using System.Linq.Expressions;
using LinqToDB.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TravelBookingPlatform.Domain.Entities;
using TravelBookingPlatform.Domain.Enums;
using TravelBookingPlatform.Domain.Exceptions;
using TravelBookingPlatform.Domain.Interfaces.Persistence.Repositories;
using TravelBookingPlatform.Domain.Messages;
using TravelBookingPlatform.Domain.Model;
using TravelBookingPlatform.Domain.Messages;
using TravelBookingPlatform.Infrastructure.Persistence.DbContexts;
using TravelBookingPlatform.Infrastructure.Persistence.Extensions;
using TravelBookingPlatform.Infrastructure.Persistence.Helpers;

namespace TravelBookingPlatform.Infrastructure.Persistence.Repositories;

public class CityRepository : ICityRepository
{
    private readonly HotelBookingDbContext _context;

    public CityRepository(HotelBookingDbContext context)
    {
        _context = context;
    }

    public async Task<bool> ExistsAsync(Expression<Func<City, bool>> predicate, CancellationToken cancellationToken = default)
    {
        return await _context.Cities.AnyAsync(predicate, cancellationToken);
    }

    public async Task<PaginatedResult<CityInformations>> GetForManagementAsync(EntityQuery<City> query,
        CancellationToken cancellationToken = default)
    {
        var queryable = _context.Cities
            .Where(query.Filter)
            .SortByExpression(SortingExpressions.GetCitySortExpression(query.SortColumn), query.SortOrder)
            .Select(c => new CityInformations
            {
                Id = c.Id,
                Country = c.Country,
                Name = c.Name,
                PostOffice = c.PostOffice,
                NumberOfHotels = c.Hotels.Count,
                CreatedAtUtc = c.CreatedAtUtc,
                ModifiedAtUtc = c.ModifiedAtUtc
            });

        var itemsToReturn = await queryable
            .GetPage(query.PageNumber, query.PageSize)
            .ToListAsync(cancellationToken);

        return new PaginatedResult<CityInformations>(
            itemsToReturn,
            await queryable.GetPaginationMetadataAsync(query.PageNumber, query.PageSize));
    }

    public async Task<City?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _context.Cities.FindAsync(new object[] { id }, cancellationToken);
    }

    public async Task<City> CreateAsync(City city, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(city);    
        var createdCity = await _context.Cities.AddAsync(city, cancellationToken);
        return createdCity.Entity;
    }

    public async Task DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        if (!await _context.Cities.AnyAsync(r => r.Id == id, cancellationToken))
        {
            throw new NotFoundException(CityMessages.NotFoundById);
        }
        
        var entity = _context.ChangeTracker.Entries<City>()
                      .FirstOrDefault(e => e.Entity.Id == id)?.Entity
                    ?? new City { Id = id };

        _context.Cities.Remove(entity);
    }

    public async Task UpdateAsync(City city, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(city);    
        if (!await _context.Cities.AnyAsync(c => c.Id == city.Id, cancellationToken))
        {
            throw new NotFoundException(CityMessages.NotFoundById);
        }

        _context.Cities.Update(city);
    }

    public async Task<IEnumerable<City>> GetMostVisitedAsync(int count, CancellationToken cancellationToken = default)
    {
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(count);

        var mostVisitedCityIds = await _context.Bookings
            .GroupBy(b => b.Hotel.CityId)
            .OrderByDescending(g => g.Count())
            .Take(count)
            .Select(g => g.Key)
            .ToListAsync(cancellationToken);

        var mostVisitedCities = await _context.Cities
            .Where(c => mostVisitedCityIds.Contains(c.Id))
            .AsNoTracking()
            .ToListAsync(cancellationToken);

        var thumbnails = await _context.Images
            .Where(i => mostVisitedCityIds.Contains(i.EntityId) && i.Type == ImageType.Thumbnail)
            .AsNoTracking()
            .ToListAsync(cancellationToken);

        var cityThumbnails = thumbnails.GroupBy(i => i.EntityId)
                                        .ToDictionary(g => g.Key, g => g.FirstOrDefault());

        foreach (var city in mostVisitedCities)
        {
            city.Thumbnail = cityThumbnails.ContainsKey(city.Id)
                ? cityThumbnails[city.Id]
                : null;
        }

        return mostVisitedCities;
    }
}