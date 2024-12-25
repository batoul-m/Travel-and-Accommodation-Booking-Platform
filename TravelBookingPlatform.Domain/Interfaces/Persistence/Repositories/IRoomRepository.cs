using System.Linq.Expressions;
using TravelBookingPlatform.Domain.Entities;
using TravelBookingPlatform.Domain.Model;

namespace TravelBookingPlatform.Domain.Interfaces.Persistence.Repositories;

public interface IRoomRepository
{
  Task<bool> ExistsAsync(Expression<Func<Room, bool>> predicate,
                         CancellationToken cancellationToken = default);
  
  Task<PaginatedResult<RoomInformations>> GetForManagementAsync(
    EntityQuery<Room> query,
    CancellationToken cancellationToken = default);

  Task<Room?> GetByIdAsync(Guid roomClassId, Guid id, CancellationToken cancellationToken = default);

  Task<Room> CreateAsync(Room room, CancellationToken cancellationToken = default);

  Task UpdateAsync(Room room, CancellationToken cancellationToken = default);

  Task DeleteAsync(Guid id, CancellationToken cancellationToken = default);

  Task<PaginatedResult<Room>> GetAsync(EntityQuery<Room> query, CancellationToken cancellationToken = default);
  
  Task<Room?> GetByIdWithRoomClassAsync(Guid roomId, CancellationToken cancellationToken = default);
}