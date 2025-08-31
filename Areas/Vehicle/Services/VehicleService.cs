using AutoMapper;
using CarRentalSystemSeparation.Areas.Vehicle.Models;
using CarRentalSystemSeparation.Areas.Vehicle.DTOs;
using CarRentalSystemSeparation.Areas.Vehicle.ViewModels;
using CarRentalSystemSeparation.Areas.Vehicle.Repositories;
using CarRentalSystemSeparation.Common.Enums;


namespace CarRentalSystemSeparation.Areas.Vehicle.Services
{
    public interface IVehicleService
    {
        Task<IEnumerable<VehicleListDTO>> GetAllVehiclesAsync();
        Task<IEnumerable<VehicleListDTO>> GetAvailableVehiclesAsync();
        Task<VehicleDTO?> GetVehicleByIdAsync(int id);
        Task<IEnumerable<VehicleListDTO>> GetVehiclesByTypeAsync(VehicleType type);
        Task<VehicleDTO?> CreateVehicleAsync(VehicleViewModel viewModel);
        Task<VehicleDTO?> UpdateVehicleAsync(int id, VehicleViewModel viewModel);
        Task<bool> DeleteVehicleAsync(int id);
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

        public async Task<IEnumerable<VehicleListDTO>> GetAllVehiclesAsync()
        {
            var vehicles = await _vehicleRepository.GetAllVehiclesAsync();
            return _mapper.Map<IEnumerable<VehicleListDTO>>(vehicles);
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

        public async Task<VehicleDTO?> CreateVehicleAsync(VehicleViewModel viewModel)
        {
            
            var vehicle = _mapper.Map<Models.Vehicle>(viewModel);
            var createdVehicle = await _vehicleRepository.CreateAsync(vehicle);
            return _mapper.Map<VehicleDTO>(createdVehicle);
        }

        public async Task<VehicleDTO?> UpdateVehicleAsync(int id, VehicleViewModel viewModel)
        {
            var existingVehicle = await _vehicleRepository.GetByIdAsync(id);
            if (existingVehicle == null)
                return null;

            // Update vehicle properties
            existingVehicle.Make = viewModel.Make;
            existingVehicle.Model = viewModel.Model;
            existingVehicle.Year = viewModel.Year;
            existingVehicle.Type = viewModel.Type;
            existingVehicle.PricePerDay = viewModel.PricePerDay;
            existingVehicle.ImageUrl = viewModel.ImageUrl;
            existingVehicle.Status = viewModel.Status;
            existingVehicle.Description = viewModel.Description;
            existingVehicle.Features = viewModel.Features;

            var updatedVehicle = await _vehicleRepository.UpdateAsync(existingVehicle);
            return _mapper.Map<VehicleDTO>(updatedVehicle);
        }

        public async Task<bool> DeleteVehicleAsync(int id)
        {
            return await _vehicleRepository.DeleteAsync(id);
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