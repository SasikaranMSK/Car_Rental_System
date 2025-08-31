using AutoMapper;
using CarRentalSystemSeparation.Areas.Customer.Models;
using CarRentalSystemSeparation.Areas.Customer.DTOs;
using CarRentalSystemSeparation.Areas.Customer.ViewModels;
using CarRentalSystemSeparation.Areas.Customer.Repositories;
using CarRentalSystemSeparation.Areas.Vehicle.Repositories;
using CarRentalSystemSeparation.Common.Enums;

namespace CarRentalSystemSeparation.Areas.Customer.Services
{
    public interface IBookingService
    {
        Task<IEnumerable<BookingListDTO>> GetUserBookingsAsync(int userId);
        Task<BookingDTO?> GetBookingByIdAsync(int id);
        Task<BookingDTO?> CreateBookingAsync(BookingViewModel viewModel, int userId);
        Task<BookingDTO?> UpdateBookingAsync(int id, BookingViewModel viewModel);
        Task<bool> CancelBookingAsync(int id);
        Task<bool> IsVehicleAvailableAsync(int vehicleId, DateTime pickupDate, DateTime returnDate);
    }

    public class BookingService : IBookingService
    {
        private readonly IBookingRepository _bookingRepository;
        private readonly IVehicleRepository _vehicleRepository;
        private readonly IMapper _mapper;

        public BookingService(IBookingRepository bookingRepository, IVehicleRepository vehicleRepository, IMapper mapper)
        {
            _bookingRepository = bookingRepository;
            _vehicleRepository = vehicleRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<BookingListDTO>> GetUserBookingsAsync(int userId)
        {
            var bookings = await _bookingRepository.GetByUserIdAsync(userId);
            return _mapper.Map<IEnumerable<BookingListDTO>>(bookings);
        }

        public async Task<BookingDTO?> GetBookingByIdAsync(int id)
        {
            var booking = await _bookingRepository.GetByIdAsync(id);
            if (booking == null)
                return null;

            return _mapper.Map<BookingDTO>(booking);
        }

        public async Task<BookingDTO?> CreateBookingAsync(BookingViewModel viewModel, int userId)
        {
            // Business rule: Check if vehicle is available
            var isAvailable = await _bookingRepository.IsVehicleAvailableAsync(
                viewModel.VehicleId, viewModel.PickupDate, viewModel.ReturnDate);

            if (!isAvailable)
                return null;

            // Business rule: Get vehicle to calculate total amount
            var vehicle = await _vehicleRepository.GetByIdAsync(viewModel.VehicleId);
            if (vehicle == null || vehicle.Status != VehicleStatus.Available)
                return null;

            var booking = _mapper.Map<CarRentalSystemSeparation.Areas.Customer.Models.Booking>(viewModel);

            booking.UserId = userId;

            // Business rule: Calculate total amount
            var totalDays = (viewModel.ReturnDate.Date - viewModel.PickupDate.Date).Days + 1;
            booking.TotalAmount = vehicle.PricePerDay * totalDays;

            await _bookingRepository.CreateAsync(booking);
            return _mapper.Map<BookingDTO>(booking);
        }

        public async Task<BookingDTO?> UpdateBookingAsync(int id, BookingViewModel viewModel)
        {
            var existingBooking = await _bookingRepository.GetByIdAsync(id);
            if (existingBooking == null)
                return null;

            // Business rule: Only allow updates for pending bookings
            if (existingBooking.Status != BookingStatus.Pending)
                return null;

            // Update booking properties
            existingBooking.PickupDate = viewModel.PickupDate;
            existingBooking.ReturnDate = viewModel.ReturnDate;
            existingBooking.PickupTime = viewModel.PickupTime;
            existingBooking.Notes = viewModel.Notes;

            // Recalculate total amount
            var vehicle = await _vehicleRepository.GetByIdAsync(existingBooking.VehicleId);
            if (vehicle != null)
            {
                var totalDays = (viewModel.ReturnDate.Date - viewModel.PickupDate.Date).Days + 1;
                existingBooking.TotalAmount = (int)(vehicle.PricePerDay * totalDays);
            }

            var updatedBooking = await _bookingRepository.UpdateAsync(existingBooking);
            return _mapper.Map<BookingDTO>(updatedBooking);
        }

        public async Task<bool> CancelBookingAsync(int id)
        {
            var booking = await _bookingRepository.GetByIdAsync(id);
            if (booking == null)
                return false;

            // Business rule: Only allow cancellation for pending or confirmed bookings
            if (booking.Status == BookingStatus.Cancelled || booking.Status == BookingStatus.Completed)
                return false;

            booking.Status = BookingStatus.Cancelled;
            await _bookingRepository.UpdateAsync(booking);
            return true;
        }

        public async Task<bool> IsVehicleAvailableAsync(int vehicleId, DateTime pickupDate, DateTime returnDate)
        {
            return await _bookingRepository.IsVehicleAvailableAsync(vehicleId, pickupDate, returnDate);
        }
    }
}