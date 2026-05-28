using Domain.Entities;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class ReviewRepository
    {
        private readonly AppDbContext _context;

        public ReviewRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Review?> GetByIdAsync(int id)
        {
            return await _context.Reviews.FindAsync(id);
        }

        public async Task<IEnumerable<Review>> GetByPropertyIdAsync(int propertyId)
        {
            return await _context.Reviews
                .Where(r => r.PropertyId == propertyId)
                .ToListAsync();
        }

        public async Task<Review> AddAsync(Review review)
        {
            await _context.Reviews.AddAsync(review);
            await _context.SaveChangesAsync();
            return review;
        }

        public async Task UpdateAsync(Review review)
        {
            _context.Reviews.Update(review);
            await _context.SaveChangesAsync();
        }

        public async Task<Review?> FindByIdAsync(int reviewId)
        {
            return await _context.Reviews.FindAsync(reviewId);
        }

        public async Task<bool> ExistsAsync(int reviewId)
        {
            return await _context.Reviews.AnyAsync(r => r.Id == reviewId);
        }

        public async Task<Review> GetByUserAndPropertyAsync(int userId, int propertyId)
        {
            return await _context.Reviews
                .FirstOrDefaultAsync(r => r.UserId == userId && r.PropertyId == propertyId);
        }

        public async Task<bool> HasUserCompletedReservationAsync(int userId, int propertyId)
        {
            return await _context.Reservations
                .AnyAsync(reservation => 
                    reservation.GuestId == userId && 
                    reservation.PropertyId == propertyId && 
                    reservation.EndDate < DateOnly.FromDateTime(DateTime.UtcNow));
        }

        public async Task<bool> ExistsByUserIdAndPropertyIdAsync(int userId, int propertyId)
        {
            return await _context.Reviews
                .AnyAsync(r => r.UserId == userId && r.PropertyId == propertyId);
        }

        public async Task<int> GetTotalReviews()
        {
            return await _context.Reviews.CountAsync();
        }
    }
}
