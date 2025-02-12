using System.Linq.Expressions;
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

public class HotelRepository : IHotelRepository
{
    private readonly HotelBookingDbContext _context;

    public HotelRepository(HotelBookingDbContext context)
    {
        _context = context;
    }

    public async Task<bool> ExistsAsync(Expression<Func<Hotel, bool>> predicate, CancellationToken cancellationToken = default)
    {
        return await _context.Hotels.AnyAsync(predicate, cancellationToken);
    }
  
    public async Task<PaginatedResult<HotelInformations>> GetForManagementAsync(
        EntityQuery<Hotel> query,
        CancellationToken cancellationToken = default)
    {
        var queryable = _context.Hotels
            .Where(query.Filter)
            .SortByExpression(SortingExpressions.GetHotelSortExpression(query.SortColumn), query.SortOrder)
            .Select(h => new HotelInformations
            {
                Id = h.Id,
                Name = h.Name,
                StarRating = h.StarRating,
                Owner = h.Owner,
                NumberOfRooms = h.RoomCategory.SelectMany(rc => rc.Rooms).Count(),
                CreatedAtUtc = h.CreatedAtUtc,
                ModifiedAtUtc = h.ModifiedAtUtc
            });

        var itemsToReturn = await queryable
            .GetPage(query.PageNumber, query.PageSize)
            .ToListAsync(cancellationToken);

        return new PaginatedResult<HotelInformations>(
            itemsToReturn,
            await queryable.GetPaginationMetadataAsync(
                query.PageNumber,
                query.PageSize));
    }

    public async Task<Hotel?> GetByIdAsync(Guid id, 
        bool includeCity = false,
        bool includeThumbnail = false,
        bool includeGallery = false,
        CancellationToken cancellationToken = default)
    {
        var query = _context.Hotels
            .Where(h => h.Id == id);

        if (includeCity)
        {
            query.Include(h => h.City);
        }

        var hotel = await query.FirstOrDefaultAsync(cancellationToken);

        if (hotel is null)
        {
            return hotel;
        }

        if (includeThumbnail)
        {
            hotel.Thumbnail = await _context.Images.FirstOrDefaultAsync(
                i => i.EntityId == hotel.Id && i.Type == ImageType.Thumbnail, cancellationToken);
        }
        
        if (includeGallery)
        {
            hotel.Gallery = await _context.Images.Where(
                i => i.EntityId == hotel.Id && i.Type == ImageType.Gallery)
              .ToListAsync(cancellationToken);
        }

        return hotel;
    }

    public async Task<Hotel> CreateAsync(Hotel hotel, CancellationToken cancellationToken = default)
    {
        var createdHotel = await _context.Hotels.AddAsync(hotel, cancellationToken);
        return createdHotel.Entity;
    }

    public async Task UpdateAsync(Hotel hotel, CancellationToken cancellationToken = default)
    {
        if (!await _context.Hotels.AnyAsync(h => h.Id == hotel.Id, cancellationToken))
        {
            throw new NotFoundException(HotelMessages.NotFoundById);
        }

        _context.Hotels.Update(hotel);
    }

    public async Task DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        if (!await _context.Hotels.AnyAsync(r => r.Id == id, cancellationToken))
        {
            throw new NotFoundException(HotelMessages.NotFoundById);
        }

        var hotelEntity = _context.ChangeTracker.Entries<Hotel>()
                           .FirstOrDefault(e => e.Entity.Id == id)?.Entity
                         ?? new Hotel { Id = id };

        _context.Hotels.Remove(hotelEntity);
    }

    public async Task<PaginatedResult<HotelSearchResult>> GetForSearchAsync(EntityQuery<Hotel> query, CancellationToken cancellationToken = default)
    {
        var queryable = _context.Hotels
            .Where(query.Filter)
            .Select(h => new HotelSearchResult
            {
                Id = h.Id,
                Name = h.Name,
                StarRating = h.StarRating,
                ReviewsRating = h.ReviewsRating,
                BriefDescription = h.BriefDescription,
                PricePerNightStartingAt = h.RoomCategory.Min(rc => rc.PricePerNight)
            })
            .SortByExpression(SortingExpressions.GetHotelSearchSortExpression(query.SortColumn), query.SortOrder);

        var requestedPage = queryable.GetPage(
            query.PageNumber, query.PageSize);
        
        var itemsToReturn = (await requestedPage.Select(h => new
            {
                Hotel = h,
                Thumbnail = _context.Images
                    .Where(i => i.EntityId == h.Id && i.Type == ImageType.Thumbnail)
                    .ToList()
            }).ToListAsync(cancellationToken))
            .Select(h =>
            {
                h.Hotel.Thumbnail = h.Thumbnail.FirstOrDefault();
        
                return h.Hotel;
            });

        return new PaginatedResult<HotelSearchResult>(
            itemsToReturn,
            await queryable.GetPaginationMetadataAsync(
                query.PageNumber,
                query.PageSize));
    }

    public async Task UpdateReviewById(Guid id, double newRating, CancellationToken cancellationToken = default)
    {
        if (!await _context.Hotels.AnyAsync(h => h.Id == id, cancellationToken))
        {
            throw new NotFoundException(HotelMessages.NotFoundById);
        }
        
        var entity = _context.ChangeTracker.Entries<Hotel>()
                       .FirstOrDefault(e => e.Entity.Id == id)?.Entity
                     ?? new Hotel { Id = id };

        entity.ReviewsRating = newRating;

        _context.Hotels.Update(entity);
    }
}