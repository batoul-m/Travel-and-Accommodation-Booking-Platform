using Microsoft.AspNetCore.Http;
using TravelBookingPlatform.Domain.Entities;

namespace TravelBookingPlatform.Domain.Interfaces.Persistence.Services;

public interface IImageService
{
  Task<Image> StoreAsync(
    IFormFile image,
    CancellationToken cancellationToken = default);

  Task DeleteAsync(
    Image image,
    CancellationToken cancellationToken = default);
}