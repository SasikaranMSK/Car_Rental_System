using AutoMapper;
using CarRentalSystemSeparation.Areas.Booking.Models;
using CarRentalSystemSeparation.Areas.Booking.DTOs;
using CarRentalSystemSeparation.Areas.Booking.ViewModels;
using CarRentalSystemSeparation.Areas.Booking.Repositories;
using CarRentalSystemSeparation.Areas.Customer.Repositories;
using CarRentalSystemSeparation.Common.Enums;

namespace CarRentalSystemSeparation.Areas.Booking.Services
{
    public interface IRentalService
    {
        Task<IEnumerable<RentalListDTO>> GetAllRentalsAsync();
        Task<RentalDTO?> GetRentalByIdAsync(int id);
        Task<RentalDTO?> CreateRentalAsync(RentalViewModel viewModel);
        Task<RentalDTO?> UpdateRentalAsync(int id, RentalViewModel viewModel);
        Task<bool> CompleteRentalAsync(int id, string returnNotes);
        Task<bool> DeleteRentalAsync(int id);
    }

    public class RentalService : IRentalService
    {
        private readonly IRentalRepository _rentalRepository;
        private readonly IBookingRepository _bookingRepository;
        private readonly IMapper _mapper;

        public RentalService(IRentalRepository rentalRepository, IBookingRepository bookingRepository, IMapper mapper)
        {
            _rentalRepository = rentalRepository;
            _bookingRepository = bookingRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<RentalListDTO>> GetAllRentalsAsync()
        {
            var rentals = await _rentalRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<RentalListDTO>>(rentals);
        }

        public async Task<RentalDTO?> GetRentalByIdAsync(int id)
        {
            var rental = await _rentalRepository.GetByIdAsync(id);
            if (rental == null)
                return null;

            return _mapper.Map<RentalDTO>(rental);
        }

        public async Task<RentalDTO?> CreateRentalAsync(RentalViewModel viewModel)
        {
            // Business rule: Check if booking exists and is confirmed
            var booking = await _bookingRepository.GetByIdAsync(viewModel.BookingId);
            if (booking == null || booking.Status != BookingStatus.Confirmed)
                return null;

            // Business rule: Check if rental already exists for this booking
            var existingRental = await _rentalRepository.GetByBookingIdAsync(viewModel.BookingId);
            if (existingRental != null)
                return null;

            var rental = _mapper.Map<Rental>(viewModel);
            var createdRental = await _rentalRepository.CreateAsync(rental);
            
            // Update booking status to in progress
            booking.Status = BookingStatus.InProgress;
            await _bookingRepository.UpdateAsync(booking);

            return _mapper.Map<RentalDTO>(createdRental);
        }

        public async Task<RentalDTO?> UpdateRentalAsync(int id, RentalViewModel viewModel)
        {
            var existingRental = await _rentalRepository.GetByIdAsync(id);
            if (existingRental == null)
                return null;

            // Business rule: Only allow updates for active rentals
            if (existingRental.Status != RentalStatus.Active)
                return null;

            // Update rental properties
            existingRental.ActualPickupDate = viewModel.ActualPickupDate;
            existingRental.ActualReturnDate = viewModel.ActualReturnDate;
            existingRental.ActualAmount = viewModel.ActualAmount;
            existingRental.PickupNotes = viewModel.PickupNotes;
            existingRental.ReturnNotes = viewModel.ReturnNotes;
            existingRental.Status = viewModel.Status;

            var updatedRental = await _rentalRepository.UpdateAsync(existingRental);
            return _mapper.Map<RentalDTO>(updatedRental);
        }

        public async Task<bool> CompleteRentalAsync(int id, string returnNotes)
        {
            var rental = await _rentalRepository.GetByIdAsync(id);
            if (rental == null || rental.Status != RentalStatus.Active)
                return false;

            rental.ActualReturnDate = DateTime.UtcNow;
            rental.ReturnNotes = returnNotes;
            rental.Status = RentalStatus.Completed;

            await _rentalRepository.UpdateAsync(rental);

            // Update booking status to completed
            var booking = await _bookingRepository.GetByIdAsync(rental.BookingId);
            if (booking != null)
            {
                booking.Status = BookingStatus.Completed;
                await _bookingRepository.UpdateAsync(booking);
            }

            return true;
        }

        public async Task<bool> DeleteRentalAsync(int id)
        {
            return await _rentalRepository.DeleteAsync(id);
        }
    }
}