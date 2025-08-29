using AutoMapper;
using CarRentalSystemSeparation.Areas.Vehicle.Models;
using CarRentalSystemSeparation.Areas.Vehicle.DTOs;
using CarRentalSystemSeparation.Areas.Vehicle.Repositories;
using CarRentalSystemSeparation.Common.Enums;

namespace CarRentalSystemSeparation.Areas.Vehicle.Services
{
    public interface IVehicleService
    {
        Task<IEnumerable<VehicleListDTO>> GetAvailableVehiclesAsync();
        Task<VehicleDTO?> GetVehicleByIdAsync(int id);
        Task<IEnumerable<VehicleListDTO>> GetVehiclesByTypeAsync(VehicleType type);
    }

    public interface IBannerService
    {
        Task<IEnumerable<BannerDTO>> GetActiveBannersAsync();
    }

    public class VehicleService : IVehicleService
    {
        private readonly IVehicleRepository _vehicleRepository;
        private readonly IMapper _mapper;

        public VehicleService(IVehicleRepository vehicleRepository, IMapper mapper)
        {
            _vehicleRepository = vehicleRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<VehicleListDTO>> GetAvailableVehiclesAsync()
        {
            var vehicles = await _vehicleRepository.GetAvailableVehiclesAsync();
            return _mapper.Map<IEnumerable<VehicleListDTO>>(vehicles);
        }

        public async Task<VehicleDTO?> GetVehicleByIdAsync(int id)
        {
            var vehicle = await _vehicleRepository.GetByIdAsync(id);
            if (vehicle == null)
                return null;

            return _mapper.Map<VehicleDTO>(vehicle);
        }

        public async Task<IEnumerable<VehicleListDTO>> GetVehiclesByTypeAsync(VehicleType type)
        {
            var vehicles = await _vehicleRepository.GetByTypeAsync(type);
            return _mapper.Map<IEnumerable<VehicleListDTO>>(vehicles);
        }
    }

    public class BannerService : IBannerService
    {
        private readonly IBannerRepository _bannerRepository;
        private readonly IMapper _mapper;

        public BannerService(IBannerRepository bannerRepository, IMapper mapper)
        {
            _bannerRepository = bannerRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<BannerDTO>> GetActiveBannersAsync()
        {
            var banners = await _bannerRepository.GetActiveBannersAsync();
            return _mapper.Map<IEnumerable<BannerDTO>>(banners);
        }
    }
}