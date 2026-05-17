using Domain.Entities;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class PropertyImageRepository
    {
        private readonly AppDbContext _context;

        public PropertyImageRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<PropertyImage>> GetByPropertyIdAsync(int propertyId)
        {
            return await _context.PropertyImages
                .Where(img => img.PropertyId == propertyId)
                .ToListAsync();
        }

        public async Task<PropertyImage?> GetByIdWithPropertyAsync(int imageId)
        {
            return await _context.PropertyImages
                .Include(pi => pi.Property)
                .FirstOrDefaultAsync(pi => pi.Id == imageId);
        }

        public async Task<PropertyImage> AddAsync(PropertyImage image)
        {
            await _context.PropertyImages.AddAsync(image);
            await _context.SaveChangesAsync();
            return image;
        }

        public async Task AddRangeAsync(List<PropertyImage> images)
        {
            await _context.PropertyImages.AddRangeAsync(images);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(PropertyImage image)
        {
            _context.PropertyImages.Update(image);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(PropertyImage image)
        {
            _context.PropertyImages.Remove(image);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> HasMainImageAsync(int propertyId)
        {
            return await _context.PropertyImages
                .AnyAsync(img => img.PropertyId == propertyId && img.IsMainImage);
        }

        public async Task<PropertyImage?> GetMainImageByPropertyIdAsync(int propertyId)
        {
            return await _context.PropertyImages
                .FirstOrDefaultAsync(img => img.PropertyId == propertyId && img.IsMainImage);
        }
    }
}
