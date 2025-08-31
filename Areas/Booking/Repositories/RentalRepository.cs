using Microsoft.EntityFrameworkCore;
using CarRentalSystemSeparation.Common.Data;
using CarRentalSystemSeparation.Areas.Booking.Models;

namespace CarRentalSystemSeparation.Areas.Booking.Repositories
{
    public interface IRentalRepository
    {
        Task<IEnumerable<Rental>> GetAllAsync();
        Task<Rental?> GetByIdAsync(int id);
        Task<Rental?> GetByBookingIdAsync(int bookingId);
        Task<Rental> CreateAsync(Rental rental);
        Task<Rental> UpdateAsync(Rental rental);
        Task<bool> DeleteAsync(int id);
        Task<bool> ExistsAsync(int id);
    }

    public class RentalRepository : IRentalRepository
    {
        private readonly ApplicationDbContext _context;

        public RentalRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Rental>> GetAllAsync()
        {
            return await _context.Rentals
                .Include(r => r.Booking)
                    .ThenInclude(b => b.User)
                .Include(r => r.Booking)
                    .ThenInclude(b => b.Vehicle)
                .OrderByDescending(r => r.CreatedAt)
                .ToListAsync();
        }

        public async Task<Rental?> GetByIdAsync(int id)
        {
            return await _context.Rentals
                .Include(r => r.Booking)
                    .ThenInclude(b => b.User)
                .Include(r => r.Booking)
                    .ThenInclude(b => b.Vehicle)
                .FirstOrDefaultAsync(r => r.Id == id);
        }

        public async Task<Rental?> GetByBookingIdAsync(int bookingId)
        {
            return await _context.Rentals
                .Include(r => r.Booking)
                    .ThenInclude(b => b.User)
                .Include(r => r.Booking)
                    .ThenInclude(b => b.Vehicle)
                .FirstOrDefaultAsync(r => r.BookingId == bookingId);
        }

        public async Task<Rental> CreateAsync(Rental rental)
        {
            rental.CreatedAt = DateTime.UtcNow;
            _context.Rentals.Add(rental);
            await _context.SaveChangesAsync();
            return rental;
        }

        public async Task<Rental> UpdateAsync(Rental rental)
        {
            rental.UpdatedAt = DateTime.UtcNow;
            _context.Rentals.Update(rental);
            await _context.SaveChangesAsync();
            return rental;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var rental = await _context.Rentals.FindAsync(id);
            if (rental == null)
                return false;

            _context.Rentals.Remove(rental);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> ExistsAsync(int id)
        {
            return await _context.Rentals.AnyAsync(r => r.Id == id);
        }
    }
}