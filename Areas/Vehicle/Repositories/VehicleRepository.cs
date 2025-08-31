using Microsoft.EntityFrameworkCore;
using CarRentalSystemSeparation.Common.Data;
using CarRentalSystemSeparation.Areas.Vehicle.Models;
using CarRentalSystemSeparation.Common.Enums;

namespace CarRentalSystemSeparation.Areas.Vehicle.Repositories
{
    public interface IVehicleRepository
    {
        Task<IEnumerable<Models.Vehicle>> GetAllVehiclesAsync();
        Task<IEnumerable<Models.Vehicle>> GetAvailableVehiclesAsync();
        Task<Models.Vehicle?> GetByIdAsync(int id);
        Task<IEnumerable<Models.Vehicle>> GetByTypeAsync(VehicleType type);
        Task<Models.Vehicle> CreateAsync(Models.Vehicle vehicle);
        Task<Models.Vehicle> UpdateAsync(Models.Vehicle vehicle);
        Task<bool> DeleteAsync(int id);
    }

    public interface IBannerRepository
    {
        Task<IEnumerable<Banner>> GetActiveBannersAsync();
    }

    public class VehicleRepository : IVehicleRepository
    {
        private readonly ApplicationDbContext _context;

        public VehicleRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Models.Vehicle>> GetAllVehiclesAsync()
        {
            return await _context.Vehicles
                .OrderBy(v => v.Make)
                .ThenBy(v => v.Model)
                .ToListAsync();
        }

        public async Task<IEnumerable<Models.Vehicle>> GetAvailableVehiclesAsync()
        {
            return await _context.Vehicles
                .Where(v => v.Status == VehicleStatus.Available)
                .OrderBy(v => v.PricePerDay)
                .ToListAsync();
        }

        public async Task<Models.Vehicle?> GetByIdAsync(int id)
        {
            return await _context.Vehicles.FindAsync(id);
        }

        public async Task<IEnumerable<Models.Vehicle>> GetByTypeAsync(VehicleType type)
        {
            return await _context.Vehicles
                .Where(v => v.Type == type && v.Status == VehicleStatus.Available)
                .OrderBy(v => v.PricePerDay)
                .ToListAsync();
        }

        public async Task<Models.Vehicle> CreateAsync(Models.Vehicle vehicle)
        {
            vehicle.CreatedAt = DateTime.UtcNow;
            _context.Vehicles.Add(vehicle);
            await _context.SaveChangesAsync();
            return vehicle;
        }

        public async Task<Models.Vehicle> UpdateAsync(Models.Vehicle vehicle)
        {
            vehicle.UpdatedAt = DateTime.UtcNow;
            _context.Vehicles.Update(vehicle);
            await _context.SaveChangesAsync();
            return vehicle;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var vehicle = await _context.Vehicles.FindAsync(id);
            if (vehicle == null)
                return false;

            _context.Vehicles.Remove(vehicle);
            await _context.SaveChangesAsync();
            return true;
        }
    }

    public class BannerRepository : IBannerRepository
    {
        private readonly ApplicationDbContext _context;

        public BannerRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Banner>> GetActiveBannersAsync()
        {
            return await _context.Banners
                .Where(b => b.IsActive)
                .OrderBy(b => b.DisplayOrder)
                .ToListAsync();
        }
    }
}