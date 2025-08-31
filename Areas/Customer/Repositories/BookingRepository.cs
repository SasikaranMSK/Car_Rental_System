using Microsoft.EntityFrameworkCore;
using CarRentalSystemSeparation.Common.Data;
using CarRentalSystemSeparation.Areas.Customer.Models;
using CarRentalSystemSeparation.Common.Enums;

namespace CarRentalSystemSeparation.Areas.Customer.Repositories
{
    public interface IBookingRepository
    {
        DateTime CreatedAt { get; set; }
        DateTime UpdatedAt { get; set; }
        BookingStatus Status { get; set; }
        DateTime PickupDate { get; set; }
        DateTime ReturnDate { get; set; }
        string PickupTime { get; set; }
        string Notes { get; set; }
        int TotalAmount { get; set; }
        int VehicleId { get; set; }

        Task<IEnumerable<IBookingRepository>> GetAllAsync();
        Task<IEnumerable<IBookingRepository>> GetByUserIdAsync(int userId);
        Task<IBookingRepository?> GetByIdAsync(int id);
        Task<IBookingRepository> CreateAsync(IBookingRepository booking);
        Task<IBookingRepository> UpdateAsync(IBookingRepository booking);
        Task<bool> DeleteAsync(int id);
        Task<bool> ExistsAsync(int id);
        Task<bool> IsVehicleAvailableAsync(int vehicleId, DateTime pickupDate, DateTime returnDate);
        Task CreateAsync(Models.Booking booking);
    }

    public class BookingRepository : IBookingRepository
    {
        private readonly ApplicationDbContext _context;

        public DateTime CreatedAt { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public DateTime UpdatedAt { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public BookingStatus Status { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public DateTime PickupDate { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public DateTime ReturnDate { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public string PickupTime { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public string Notes { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public int TotalAmount { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public int VehicleId { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public BookingRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<IBookingRepository>> GetAllAsync()
        {
            return (IEnumerable<IBookingRepository>)await _context.Bookings
                .Include(b => b.User)
                .Include(b => b.Vehicle)
                .OrderByDescending(b => b.CreatedAt)
                .ToListAsync();
        }

        public async Task<IEnumerable<IBookingRepository>> GetByUserIdAsync(int userId)
        {
            return (IEnumerable<IBookingRepository>)await _context.Bookings
                .Include(b => b.Vehicle)
                .Where(b => b.UserId == userId)
                .OrderByDescending(b => b.CreatedAt)
                .ToListAsync();
        }

        public async Task<IBookingRepository?> GetByIdAsync(int id)
        {
            return (IBookingRepository?)await _context.Bookings
                .Include(b => b.User)
                .Include(b => b.Vehicle)
                .FirstOrDefaultAsync(b => b.Id == id);
        }

        public async Task<IBookingRepository> CreateAsync(IBookingRepository booking)
        {
            booking.CreatedAt = DateTime.UtcNow;
            _context.Bookings.Add((Models.Booking)booking);
            await _context.SaveChangesAsync();
            return booking;
        }

        public async Task<IBookingRepository> UpdateAsync(IBookingRepository booking)
        {
            booking.UpdatedAt = DateTime.UtcNow;
            _context.Bookings.Update((Models.Booking)booking);
            await _context.SaveChangesAsync();
            return booking;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var booking = await _context.Bookings.FindAsync(id);
            if (booking == null)
                return false;

            _context.Bookings.Remove(booking);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> ExistsAsync(int id)
        {
            return await _context.Bookings.AnyAsync(b => b.Id == id);
        }

        public async Task<bool> IsVehicleAvailableAsync(int vehicleId, DateTime pickupDate, DateTime returnDate)
        {
            return !await _context.Bookings.AnyAsync(b =>
                b.VehicleId == vehicleId &&
                b.Status != Common.Enums.BookingStatus.Cancelled &&
                ((pickupDate >= b.PickupDate && pickupDate <= b.ReturnDate) ||
                 (returnDate >= b.PickupDate && returnDate <= b.ReturnDate) ||
                 (pickupDate <= b.PickupDate && returnDate >= b.ReturnDate)));
        }

        Task<IEnumerable<IBookingRepository>> IBookingRepository.GetAllAsync()
        {
            throw new NotImplementedException();
        }

        Task<IEnumerable<IBookingRepository>> IBookingRepository.GetByUserIdAsync(int userId)
        {
            throw new NotImplementedException();
        }

        public Task CreateAsync(Models.Booking booking)
        {
            throw new NotImplementedException();
        }
    }
}