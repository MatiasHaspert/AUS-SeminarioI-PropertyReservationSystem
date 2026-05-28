using Domain.Entities;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class ReservationRepository
    {
        private readonly AppDbContext _context;

        public ReservationRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Reservation> AddAsync(Reservation reservation)
        {
            _context.Reservations.Add(reservation);
            await _context.SaveChangesAsync();
            return reservation;
        }

        public async Task<List<Reservation>> GetByUserIdOrderByDateAsync(int userId)
        {
            return await _context.Reservations
                                 .Include(r => r.Property)
                                    .ThenInclude(p => p.Images)
                                 .Where(r => r.GuestId == userId)
                                 .OrderByDescending(r => r.CreatedAt)
                                 .ToListAsync();
        }

        public async Task<List<Reservation>> GetByPropertyIdForOwnerIdAsync(int propertyId, int ownerId)
        {
            return await _context.Reservations
                                 .Include(r => r.Guest)
                                 .Include(r => r.Property)
                                    .ThenInclude(p => p.Images)
                                 .Where(r => r.PropertyId == propertyId && r.Property.OwnerId == ownerId)
                                 .ToListAsync();
        }

        public async Task<Reservation?> GetByIdWithPropertyAsync(int reservationId)
        {
            return await _context.Reservations
                                 .Include(r => r.Guest)
                                 .Include(r => r.Property)
                                    .ThenInclude(p => p.Images)
                                 .FirstOrDefaultAsync(r => r.Id == reservationId);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        public async Task<Reservation?> GetByIdAsync(int reservationId)
        {
            return await _context.Reservations.FindAsync(reservationId);
        }

        public async Task<List<Reservation>> GetByOwnerIdAsync(int ownerId)
        {
            return await _context.Reservations
                                 .Include(r => r.Guest)
                                 .Include(r => r.Property)
                                    .ThenInclude(p => p.Images)
                                 .Where(r => r.Property.OwnerId == ownerId)
                                 .ToListAsync();
        }

        public async Task<Dictionary<string, int>> GetReservationsGroupedByStatusAsync()
        {
            return await _context.Reservations
                                 .GroupBy(r => r.Status.ToString())
                                 .ToDictionaryAsync(x => x.Key, x => x.Count());
        }

        public async Task<decimal> GetTotalRevenueAsync()
        {
            return await _context.Reservations
                .Where(r => r.Status == Domain.Enums.ReservationStatus.Completed)
                .SumAsync(r => r.TotalPrice);
        }
    }
}
