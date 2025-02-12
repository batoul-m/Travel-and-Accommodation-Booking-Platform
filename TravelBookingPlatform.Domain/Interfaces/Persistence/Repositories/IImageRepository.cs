using Microsoft.AspNetCore.Http;
using TravelBookingPlatform.Domain.Entities;
using TravelBookingPlatform.Domain.Enums;

namespace TravelBookingPlatform.Domain.Interfaces.Persistence.Repositories;

public interface IImageRepository
{
  Task<Image> CreateAsync(IFormFile image, Guid entityId, ImageType type,
    CancellationToken cancellationToken = default);

  Task DeleteForAsync(Guid entityId, ImageType type, CancellationToken cancellationToken = default);
}