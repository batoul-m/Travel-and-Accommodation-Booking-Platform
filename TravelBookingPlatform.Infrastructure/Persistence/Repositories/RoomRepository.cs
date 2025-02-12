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

public class RoomRepository : IRoomRepository
{
  private readonly HotelBookingDbContext _context;

  public RoomRepository(HotelBookingDbContext context)
  {
    _context = context;
  }

  public async Task<bool> ExistsAsync(Expression<Func<Room, bool>> predicate, CancellationToken cancellationToken = default)
  {
    return await _context.Rooms.AnyAsync(predicate, cancellationToken);
  }
  
  public async Task<PaginatedResult<RoomInformations>> GetForManagementAsync(
    EntityQuery<Room> query,
    CancellationToken cancellationToken = default)
  {
    var currentDateUtc = DateOnly.FromDateTime(DateTime.UtcNow);
    
    var queryable = _context.Rooms
      .Where(query.Filter)
      .SortByExpression(SortingExpressions.GetRoomSortExpression(query.SortColumn), query.SortOrder)
      .Select(r => new RoomInformations
      {
        Id = r.RoomId,
        Number = r.Number,
        IsAvailable = !r.Bookings.Any(
          b => b.CheckInDateUtc >= currentDateUtc
               && b.CheckOutDateUtc <= currentDateUtc),
        CreatedAtUtc = r.CreatedAtUtc,
        ModifiedAtUtc = r.ModifiedAtUtc
      });

    var itemsToReturn = await queryable
      .GetPage(query.PageNumber, query.PageSize)
      .ToListAsync(cancellationToken);

    return new PaginatedResult<RoomInformations>(
      itemsToReturn,
      await queryable.GetPaginationMetadataAsync(
        query.PageNumber,
        query.PageSize));
  }

  public async Task<Room?> GetByIdAsync(Guid roomCategoryId, Guid id, CancellationToken cancellationToken = default)
  {
    return await _context.Rooms
      .FirstOrDefaultAsync(r => r.RoomId == id && r.RoomCategoryId == roomCategoryId,cancellationToken);
  }

  public async Task<Room> CreateAsync(Room room, CancellationToken cancellationToken = default)
  {
    ArgumentNullException.ThrowIfNull(room);
    var createdRoom = await _context.Rooms.AddAsync(room, cancellationToken);
    return createdRoom.Entity;
  }

  public async Task UpdateAsync(Room room, CancellationToken cancellationToken = default)
  {
    ArgumentNullException.ThrowIfNull(room);

    if (!await _context.Rooms.AnyAsync(r => r.RoomId == room.RoomId, cancellationToken))
    {
      throw new NotFoundException(RoomMessages.NotFoundById);
    }
    _context.Rooms.Update(room);
  }

  public async Task DeleteAsync(Guid id, CancellationToken cancellationToken = default)
  {
    if (!await _context.Rooms.AnyAsync(r => r.RoomId == id, cancellationToken))
    {
      throw new NotFoundException(RoomMessages.NotFoundById);
    }

    var roomEntity = _context.ChangeTracker.Entries<Room>()
                       .FirstOrDefault(e => e.Entity.RoomId == id)?.Entity
                     ?? new Room { RoomId = id };

    _context.Rooms.Remove(roomEntity);
  }

  public async Task<PaginatedResult<Room>> GetAsync(EntityQuery<Room> query, CancellationToken cancellationToken = default)
  {
    var queryable = _context.Rooms
      .Where(query.Filter)
      .SortByExpression(r => r.RoomId, query.SortOrder);

    var itemsToReturn = await queryable
      .GetPage(query.PageNumber, query.PageSize)
      .AsNoTracking()
      .ToListAsync(cancellationToken);

    return new PaginatedResult<Room>(
      itemsToReturn,
      await queryable.GetPaginationMetadataAsync(
        query.PageNumber,
        query.PageSize));
  }

  public async Task<Room?> GetByIdWithRoomClassAsync(Guid roomId, CancellationToken cancellationToken = default)
  {
    var currentDateTime = DateTime.UtcNow;
    
    return await _context.Rooms
      .Include(r => r.RoomCategory)
      .ThenInclude(rc => rc.Discounts.Where(d => d.StartDateUtc <= currentDateTime && d.EndDateUtc > currentDateTime))
      .FirstOrDefaultAsync(r => r.RoomId == roomId, cancellationToken);
  }
}

