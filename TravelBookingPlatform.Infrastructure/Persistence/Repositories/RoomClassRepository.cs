using System.Linq.Expressions;
using LinqToDB.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TravelBookingPlatform.Domain.Entities;
using TravelBookingPlatform.Domain.Enums;
using TravelBookingPlatform.Domain.Exceptions;
using TravelBookingPlatform.Domain.Interfaces.Persistence.Repositories;
using TravelBookingPlatform.Domain.Messages;
using TravelBookingPlatform.Domain.Model;
using TravelBookingPlatform.Infrastructure.Persistence.DbContexts;
using TravelBookingPlatform.Infrastructure.Persistence.Extensions;
using TravelBookingPlatform.Infrastructure.Persistence.Helpers;

namespace TravelBookingPlatform.Infrastructure.Persistence.Repositories;

public class RoomClassRepository : IRoomClassRepository
{
    private readonly HotelBookingDbContext _context;

    public RoomClassRepository(HotelBookingDbContext context)
    {
        _context = context;
    }

    public async Task<bool> ExistsAsync(Expression<Func<RoomCategory, bool>> predicate, CancellationToken cancellationToken = default)
    {
        return await _context.RoomCategories.AnyAsync(predicate, cancellationToken);
    }
  
    public async Task<RoomCategory?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _context.RoomCategories.FirstOrDefaultAsync(rc => rc.Id == id, cancellationToken);
    }

    public async Task<RoomCategory> CreateAsync(RoomCategory roomCategory, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(roomCategory);
        var createdRoomClass = await _context.RoomCategories.AddAsync(roomCategory, cancellationToken);
        return createdRoomClass.Entity;
    }

    public async Task UpdateAsync(RoomCategory roomCategory, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(roomCategory);
        if (!await ExistsAsync(rc => rc.Id == roomCategory.Id, cancellationToken))
        {
            throw new NotFoundException(RoomCategoryMessages.NotFoundById);
        }

        _context.RoomCategories.Update(roomCategory);
    }

    public async Task DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        if (!await _context.RoomCategories.AnyAsync(r => r.Id == id, cancellationToken))
        {
            throw new NotFoundException(RoomCategoryMessages.NotFoundById);
        }

        var entity = _context.ChangeTracker.Entries<RoomCategory>()
                       .FirstOrDefault(e => e.Entity.Id == id)?.Entity
                     ?? new RoomCategory { Id = id };

        _context.RoomCategories.Remove(entity);
    }

    public async Task<PaginatedResult<RoomCategory>> GetAsync(
        EntityQuery<RoomCategory> query,
        bool includeGallery = false,
        CancellationToken cancellationToken = default)
    {
        var currentDateTime = DateTime.UtcNow;

        var queryable = _context.RoomCategories
            .Include(rc => rc.Discounts
                .Where(d => currentDateTime >= d.StartDateUtc && currentDateTime < d.EndDateUtc))
            .Include(rc => rc.Amenities)
            .AsSplitQuery()
            .Where(query.Filter)
            .SortByExpression(SortingExpressions.GetRoomClassSortExpression(query.SortColumn), query.SortOrder);

        var requestedPage = queryable.GetPage(query.PageNumber, query.PageSize);

        IEnumerable<RoomCategory> itemsToReturn;

        if (includeGallery)
        {
            itemsToReturn = (await requestedPage.Select(rc => new
            {
                RoomClass = rc,
                Gallery = _context.Images
                    .Where(i => i.EntityId == rc.Id && i.Type == ImageType.Gallery)
                    .ToList()
            }).AsNoTracking().ToListAsync(cancellationToken))
            .Select(rc =>
            {
                rc.RoomClass.Gallery = rc.Gallery;
                return rc.RoomClass;
            });
        }
        else
        {
            itemsToReturn = await requestedPage
                .AsNoTracking()
                .ToListAsync(cancellationToken);
        }

        return new PaginatedResult<RoomCategory>(
            itemsToReturn,
            await queryable.GetPaginationMetadataAsync(query.PageNumber, query.PageSize));
    }

    public async Task<IEnumerable<RoomCategory>> GetFeaturedDealsInDifferentHotelsAsync(int count, CancellationToken cancellationToken = default)
    {
        var currentDateTime = DateTime.UtcNow;

        var activeDiscounts = await _context.Discounts
            .Where(d => d.StartDateUtc <= currentDateTime && d.EndDateUtc > currentDateTime)
            .AsNoTracking()
            .ToListAsync(cancellationToken);

        var rankedRoomCategories = activeDiscounts
            .Join(_context.RoomCategories.AsNoTracking(),
                d => d.RoomClassId,
                rc => rc.Id,
                (d, rc) => new { RoomCategory = rc, Discount = d })
            .GroupBy(rd => rd.RoomCategory.HotelId)
            .SelectMany(g => g
                .OrderByDescending(rd => rd.Discount.Percentage)
                .ThenBy(rd => rd.RoomCategory.PricePerNight)
                .Take(1))
            .Take(count)
            .ToList();

        var hotelIds = rankedRoomCategories.Select(rc => rc.RoomCategory.HotelId).Distinct();
        var hotels = await _context.Hotels
            .Where(h => hotelIds.Contains(h.Id))
            .Include(h => h.City)
            .AsNoTracking()
            .ToListAsync(cancellationToken);

        var thumbnails = await _context.Images
            .Where(i => hotelIds.Contains(i.EntityId) && i.Type == ImageType.Thumbnail)
            .AsNoTracking()
            .ToListAsync(cancellationToken);

        var hotelThumbnails = thumbnails.GroupBy(i => i.EntityId)
                                        .ToDictionary(g => g.Key, g => g.FirstOrDefault());

        foreach (var deal in rankedRoomCategories)
        {
            var hotel = hotels.FirstOrDefault(h => h.Id == deal.RoomCategory.HotelId);
            if (hotel is not null)
            {
                hotel.Thumbnail = hotelThumbnails.ContainsKey(hotel.Id) ? hotelThumbnails[hotel.Id] : null;
                deal.RoomCategory.Hotel = hotel;
            }

            deal.RoomCategory.Discounts.Add(deal.Discount);
        }

        return rankedRoomCategories.Select(deal => deal.RoomCategory);
    }
}