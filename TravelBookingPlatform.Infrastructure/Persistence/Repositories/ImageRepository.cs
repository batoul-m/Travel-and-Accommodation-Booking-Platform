using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using TravelBookingPlatform.Domain.Entities;
using TravelBookingPlatform.Domain.Enums;
using TravelBookingPlatform.Domain.Interfaces.Persistence.Repositories;
using TravelBookingPlatform.Domain.Interfaces.Persistence.Services;
using TravelBookingPlatform.Infrastructure.Persistence.DbContexts;

namespace TravelBookingPlatform.Infrastructure.Persistence.Repositories;

public class ImageRepository : IImageRepository
{
    private readonly HotelBookingDbContext _context;
    private readonly IImageService _imageService;

    public ImageRepository(HotelBookingDbContext context, IImageService imageService)
    {
        _context = context;
        _imageService = imageService;
    }

    public async Task<Image> CreateAsync(
        IFormFile image, 
        Guid entityId, 
        ImageType type,
        CancellationToken cancellationToken = default)
    {
        var returnedImage = await _imageService.StoreAsync(image, cancellationToken);

        var imageEntity = new Image
        {
            EntityId = entityId,
            Path = returnedImage.Path,
            Format = returnedImage.Format,
            Type = type
        };

        var createdImage = await _context.Images.AddAsync(
            imageEntity, 
            cancellationToken);

        return createdImage.Entity;
    }

    public async Task DeleteForAsync(
        Guid entityId, 
        ImageType type, 
        CancellationToken cancellationToken = default)
    {
        var images = await _context.Images
            .Where(i => i.EntityId == entityId && i.Type == type)
            .ToListAsync(cancellationToken);
        
        foreach (var image in images)
        {
            await _imageService.DeleteAsync(image, cancellationToken);

            _context.Images.Remove(image);
        }
    }
}