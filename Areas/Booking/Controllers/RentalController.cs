using Microsoft.AspNetCore.Mvc;
using CarRentalSystemSeparation.Areas.Booking.Services;
using CarRentalSystemSeparation.Areas.Booking.ViewModels;

namespace CarRentalSystemSeparation.Areas.Booking.Controllers
{
    [Area("Booking")]
    public class RentalController : Controller
    {
        private readonly IRentalService _rentalService;

        public RentalController(IRentalService rentalService)
        {
            _rentalService = rentalService;
        }

        public async Task<IActionResult> Index()
        {
            var rentals = await _rentalService.GetAllRentalsAsync();
            return View(rentals);
        }

        public async Task<IActionResult> Details(int id)
        {
            var rental = await _rentalService.GetRentalByIdAsync(id);
            if (rental == null)
            {
                return NotFound();
            }

            return View(rental);
        }

        public IActionResult Create(int bookingId)
        {
            var viewModel = new RentalViewModel
            {
                BookingId = bookingId,
                ActualPickupDate = DateTime.Now
            };

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(RentalViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }

            var rental = await _rentalService.CreateRentalAsync(viewModel);
            if (rental == null)
            {
                ModelState.AddModelError("", "Unable to create rental. Please check if the booking is valid.");
                return View(viewModel);
            }

            TempData["SuccessMessage"] = "Rental created successfully!";
            return RedirectToAction(nameof(Details), new { id = rental.Id });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Complete(int id, string returnNotes = "")
        {
            var success = await _rentalService.CompleteRentalAsync(id, returnNotes);
            if (success)
            {
                TempData["SuccessMessage"] = "Rental completed successfully.";
            }
            else
            {
                TempData["ErrorMessage"] = "Unable to complete rental.";
            }

            return RedirectToAction(nameof(Index));
        }
    }
}