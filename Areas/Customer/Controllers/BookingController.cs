using Microsoft.AspNetCore.Mvc;
using CarRentalSystemSeparation.Areas.Customer.Services;
using CarRentalSystemSeparation.Areas.Customer.ViewModels;
using CarRentalSystemSeparation.Areas.Vehicle.Services;

namespace CarRentalSystemSeparation.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class BookingController : Controller
    {
        private readonly IBookingService _bookingService;
        private readonly IVehicleService _vehicleService;

        public BookingController(IBookingService bookingService, IVehicleService vehicleService)
        {
            _bookingService = bookingService;
            _vehicleService = vehicleService;
        }

        public async Task<IActionResult> Index()
        {
            // For demo purposes, using a hardcoded user ID
            // In real implementation, get from authenticated user
            var userId = 1;
            var bookings = await _bookingService.GetUserBookingsAsync(userId);
            return View(bookings);
        }

        public async Task<IActionResult> Create(int vehicleId)
        {
            var vehicle = await _vehicleService.GetVehicleByIdAsync(vehicleId);
            if (vehicle == null)
            {
                return NotFound();
            }

            var viewModel = new BookingViewModel
            {
                VehicleId = vehicleId,
                VehicleName = vehicle.DisplayName,
                PricePerDay = vehicle.PricePerDay,
                VehicleImageUrl = vehicle.ImageUrl,
                PickupDate = DateTime.Today.AddDays(1),
                ReturnDate = DateTime.Today.AddDays(2)
            };

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(BookingViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                // Reload vehicle data for display
                var vehicle = await _vehicleService.GetVehicleByIdAsync(viewModel.VehicleId);
                if (vehicle != null)
                {
                    viewModel.VehicleName = vehicle.DisplayName;
                    viewModel.PricePerDay = vehicle.PricePerDay;
                    viewModel.VehicleImageUrl = vehicle.ImageUrl;
                }
                return View(viewModel);
            }

            // For demo purposes, using a hardcoded user ID
            var userId = 1;
            var booking = await _bookingService.CreateBookingAsync(viewModel, userId);
            
            if (booking == null)
            {
                ModelState.AddModelError("", "Vehicle is not available for the selected dates.");
                return View(viewModel);
            }

            TempData["SuccessMessage"] = "Booking created successfully!";
            return RedirectToAction(nameof(Details), new { id = booking.Id });
        }

        public async Task<IActionResult> Details(int id)
        {
            var booking = await _bookingService.GetBookingByIdAsync(id);
            if (booking == null)
            {
                return NotFound();
            }

            return View(booking);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Cancel(int id)
        {
            var success = await _bookingService.CancelBookingAsync(id);
            if (success)
            {
                TempData["SuccessMessage"] = "Booking cancelled successfully.";
            }
            else
            {
                TempData["ErrorMessage"] = "Unable to cancel booking.";
            }

            return RedirectToAction(nameof(Index));
        }
    }
}