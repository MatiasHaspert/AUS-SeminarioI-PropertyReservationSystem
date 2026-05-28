using Domain.Entities;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class PropertyRepository
    {
        private readonly AppDbContext _context;
        public record TopProperty (int PropertyId, string Title, int ReservationsCount);
        public PropertyRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Property?> GetByIdAsync(int id)
        {
            return await _context.Properties
                .Include(p => p.Images)
                .Include(p => p.Reviews)
                    .ThenInclude(r => r.User)
                .Include(p => p.Amenities)
                .Include(p => p.Availabilities)
                .Include(p => p.Reservations)
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<IEnumerable<Property>> GetAllAsync()
        {
            return await _context.Properties
                .Include(p => p.Images)
                .Include(p => p.Reviews)
                .ToListAsync();
        }

        public async Task<Property> AddAsync(Property property)
        {
            await _context.Properties.AddAsync(property);
            await _context.SaveChangesAsync();
            return property;
        }

        public async Task UpdateAsync(Property property)
        {
            _context.Properties.Update(property);
            await _context.SaveChangesAsync();
        }

        public async Task<Property?> GetByIdWithAmenitiesAsync(int id)
        {
            return await _context.Properties
                .Include(p => p.Amenities)
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<Property?> GetByIdWithAvailabilitiesAndReservationsAsync(int id)
        {
            return await _context.Properties
                .Include(p => p.Availabilities)
                .Include(p => p.Reservations)
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<IEnumerable<Property>> SearchAsync(string? city, int? guests, DateOnly? checkIn, DateOnly? checkOut)
        {
            var query = _context.Properties
                .Include(p => p.Images)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(city))
            {
                var cityLower = city.ToLower().Trim();
                query = query.Where(p => p.Address != null && p.Address.City != null && 
                                        p.Address.City.ToLower().Contains(cityLower));
            }

            if (guests.HasValue)
            {
                query = query.Where(p => p.MaxGuests >= guests.Value);
            }

            if (checkIn.HasValue && checkOut.HasValue)
            {
                query = query
                    .Include(p => p.Availabilities)
                    .Include(p => p.Reservations)
                    .Where(p =>
                        // Disponibilidad que cubra el rango completo
                        p.Availabilities.Any(a => a.StartDate <= checkIn && a.EndDate >= checkOut) &&
                        // Sin reservas que se superpongan
                        !p.Reservations.Any(r => r.StartDate < checkOut && r.EndDate > checkIn)
                    );
            }

            return await query.ToListAsync();
        }

        public async Task<bool> ExistsAsync(int id)
        {
            return await _context.Properties.AnyAsync(p => p.Id == id);
        }

        public async Task<List<Property>> GetByOwnerIdAsync(int ownerId)
        {
            return await _context.Properties
                .Where(p => p.OwnerId == ownerId)
                .ToListAsync();
        }

        public async Task<Property?> GetByIdWithReservationsAsync(int propertyId)
        {
            return await _context.Properties
                .Include(p => p.Reservations)
                .FirstOrDefaultAsync(p => p.Id == propertyId);
        }

        public async Task<int> GetTotalProperties()
        {
            return await _context.Properties.CountAsync();
        }

        public async Task<List<TopProperty>> GetTopPropertiesAsync()
        {
            return await _context.Properties
                // Primero ordenamos en base al conteo de la relación (EF lo traduce a un COUNT en SQL)
                .OrderByDescending(p => p.Reservations.Count())
                // Tomamos los 5 primeros en la base de datos
                .Take(5)
                // Finalmente proyectamos al Record
                .Select(p => new TopProperty(
                    p.Id,
                    p.Title,
                    p.Reservations.Count()
                ))
                .ToListAsync();
        }
    }
}
