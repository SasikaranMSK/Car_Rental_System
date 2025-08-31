using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using CarRentalSystemSeparation.Areas.Vehicle.Services;
using CarRentalSystemSeparation.Areas.Vehicle.DTOs;

namespace CarRentalSystemSeparation.Controllers
{
    //[Authorize(Roles = "Customer")]
    public class HomeController : Controller
    {
        private readonly IVehicleService _vehicleService;
        private readonly IBannerService _bannerService;

        public HomeController(IVehicleService vehicleService, IBannerService bannerService)
        {
            _vehicleService = vehicleService;
            _bannerService = bannerService;
        }

        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            var banners = await _bannerService.GetActiveBannersAsync();
            var featuredVehicles = (await _vehicleService.GetAvailableVehiclesAsync()).Take(6);
            
            ViewBag.Banners = banners;
            ViewBag.FeaturedVehicles = featuredVehicles;
            
            return View();
        }

        [AllowAnonymous]
        public IActionResult About()
        {
            return View();
        }

        [AllowAnonymous]
        public async Task<IActionResult> Vehicles()
        {
            var vehicles = await _vehicleService.GetAvailableVehiclesAsync();
            return View(vehicles);
        }

        [AllowAnonymous]
        public async Task<IActionResult> VehicleDetails(int id)
        {
            var vehicle = await _vehicleService.GetVehicleByIdAsync(id);
            if (vehicle == null)
            {
                return NotFound();
            }
            return View(vehicle);
        }
    }
}