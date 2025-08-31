using Microsoft.AspNetCore.Mvc;
using CarRentalSystemSeparation.Areas.Vehicle.Services;
using CarRentalSystemSeparation.Areas.Vehicle.ViewModels;
using CarRentalSystemSeparation.Common.Enums;

namespace CarRentalSystemSeparation.Areas.Vehicle.Controllers
{
    [Area("Vehicle")]
    public class VehicleController : Controller
    {
        private readonly IVehicleService _vehicleService;

        public VehicleController(IVehicleService vehicleService)
        {
            _vehicleService = vehicleService;
        }

        public async Task<IActionResult> Index()
        {
            var vehicles = await _vehicleService.GetAllVehiclesAsync();
            return View(vehicles);
        }

        public async Task<IActionResult> Details(int id)
        {
            var vehicle = await _vehicleService.GetVehicleByIdAsync(id);
            if (vehicle == null)
            {
                return NotFound();
            }

            return View(vehicle);
        }

        public IActionResult Create()
        {
            var viewModel = new VehicleViewModel();
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(VehicleViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }

            var vehicle = await _vehicleService.CreateVehicleAsync(viewModel);
            if (vehicle == null)
            {
                ModelState.AddModelError("", "Unable to create vehicle.");
                return View(viewModel);
            }

            TempData["SuccessMessage"] = "Vehicle created successfully!";
            return RedirectToAction(nameof(Details), new { id = vehicle.Id });
        }

        public async Task<IActionResult> Edit(int id)
        {
            var vehicle = await _vehicleService.GetVehicleByIdAsync(id);
            if (vehicle == null)
            {
                return NotFound();
            }

            var viewModel = new VehicleViewModel
            {
                Make = vehicle.Make,
                Model = vehicle.Model,
                Year = vehicle.Year,
                Type = vehicle.Type,
                PricePerDay = vehicle.PricePerDay,
                ImageUrl = vehicle.ImageUrl,
                Status = vehicle.Status,
                Description = vehicle.Description,
                Features = vehicle.Features
            };

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, VehicleViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }

            var vehicle = await _vehicleService.UpdateVehicleAsync(id, viewModel);
            if (vehicle == null)
            {
                return NotFound();
            }

            TempData["SuccessMessage"] = "Vehicle updated successfully!";
            return RedirectToAction(nameof(Details), new { id = vehicle.Id });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var success = await _vehicleService.DeleteVehicleAsync(id);
            if (success)
            {
                TempData["SuccessMessage"] = "Vehicle deleted successfully.";
            }
            else
            {
                TempData["ErrorMessage"] = "Unable to delete vehicle.";
            }

            return RedirectToAction(nameof(Index));
        }
    }
}