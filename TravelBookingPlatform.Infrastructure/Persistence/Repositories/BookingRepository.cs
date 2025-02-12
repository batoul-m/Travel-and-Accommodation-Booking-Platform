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

public class BookingRepository : IBookingRepository
{
    private readonly HotelBookingDbContext _context;

    public BookingRepository(HotelBookingDbContext context)
    {
        _context = context;
    }

    public async Task<bool> ExistsAsync(Expression<Func<Booking, bool>> predicate, CancellationToken cancellationToken = default)
    {
        return await _context.Bookings.AnyAsync(predicate, cancellationToken);
    }

    public async Task<Booking> CreateAsync(Booking booking, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(booking);
        var createdBooking = await _context.AddAsync(booking, cancellationToken);
        return createdBooking.Entity;
    }

    public async Task<Booking?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _context.Bookings.FindAsync(id, cancellationToken);
    }

    public async Task<Booking?> GetByIdAsync(Guid id, Guid guestId, bool includeInvoice = false, CancellationToken cancellationToken = default)
    {
        var bookings = _context.Bookings
            .Where(b => b.Id == id && b.GuestId == guestId)
            .Include(b => b.Hotel);

        if (includeInvoice)
        {
            bookings.Include(b => b.Invoice);
        }
        return await bookings.FirstOrDefaultAsync(cancellationToken);
    }

    public async Task DeleteAsync(Guid id, CancellationToken cancellationToken)
    {
        if (!await _context.Bookings.AnyAsync(b => b.Id == id, cancellationToken))
        {
            throw new NotFoundException(BookingMessages.NotFoundById);
        }

        var entity = _context.ChangeTracker.Entries<Booking>()
                    .FirstOrDefault(e => e.Entity.Id == id)?.Entity
                    ?? new Booking { Id = id };
        _context.Bookings.Remove(entity);
    }

    public async Task<PaginatedResult<Booking>> GetAsync(EntityQuery<Booking> query, CancellationToken cancellationToken = default)
    {
        var queryable = _context.Bookings
            .Where(query.Filter)
            .SortByExpression(SortingExpressions.GetBookingSortExpression(query.SortColumn), query.SortOrder);

        var itemsToReturn = await queryable
            .GetPage(query.PageNumber, query.PageSize)
            .AsNoTracking()
            .ToListAsync(cancellationToken);

        var metadata = await queryable.GetPaginationMetadataAsync(
            query.PageNumber,
            query.PageSize);

        return new PaginatedResult<Booking>(itemsToReturn, metadata);
    }

    public async Task<IEnumerable<Booking>> GetRecentBookingsInDifferentHotelsByGuestId(
        Guid guestId, int count, CancellationToken cancellationToken = default)
    {
        var guestBookings = await _context.Bookings
            .Where(b => b.GuestId == guestId)
            .AsNoTracking()
            .ToListAsync(cancellationToken);

        var recentBookingsPerHotel = guestBookings
            .GroupBy(b => b.HotelId)
            .Select(g => g.OrderByDescending(b => b.CheckInDateUtc).First())
            .Take(count)
            .ToList();

        var hotelIds = recentBookingsPerHotel.Select(b => b.HotelId).Distinct();
        var hotels = await _context.Hotels
            .Include(h => h.City)
            .Where(h => hotelIds.Contains(h.Id))
            .AsNoTracking()
            .ToListAsync(cancellationToken);

        var thumbnails = await _context.Images
            .Where(i => hotelIds.Contains(i.EntityId) && i.Type == ImageType.Thumbnail)
            .AsNoTracking()
            .ToListAsync(cancellationToken);

        var hotelThumbnails = thumbnails.GroupBy(i => i.EntityId)
                                        .ToDictionary(g => g.Key, g => g.FirstOrDefault());

        foreach (var booking in recentBookingsPerHotel)
        {
            var hotel = hotels.FirstOrDefault(h => h.Id == booking.HotelId);
            if (hotel is not null)
            {
                hotel.Thumbnail = hotelThumbnails.ContainsKey(hotel.Id)
                    ? hotelThumbnails[hotel.Id]
                    : null;
                booking.Hotel = hotel;
            }
        }

        return recentBookingsPerHotel;
    }
}