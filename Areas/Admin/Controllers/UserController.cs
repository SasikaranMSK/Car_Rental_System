using Microsoft.AspNetCore.Mvc;
using CarRentalSystemSeparation.Areas.Admin.Services;
using CarRentalSystemSeparation.Areas.Admin.ViewModels;

namespace CarRentalSystemSeparation.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class UserController : Controller
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<IActionResult> Index()
        {
            var users = await _userService.GetAllUsersAsync();
            return View(users);
        }

        public async Task<IActionResult> Details(int id)
        {
            var user = await _userService.GetUserByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        public IActionResult Create()
        {
            var viewModel = new UserViewModel();
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(UserViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }

            var user = await _userService.CreateUserAsync(viewModel);
            if (user == null)
            {
                ModelState.AddModelError("Email", "A user with this email already exists.");
                return View(viewModel);
            }

            TempData["SuccessMessage"] = "User created successfully!";
            return RedirectToAction(nameof(Details), new { id = user.Id });
        }

        public async Task<IActionResult> Edit(int id)
        {
            var user = await _userService.GetUserByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            var viewModel = new UserViewModel
            {
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Role = user.Role,
                IsActive = user.IsActive
            };

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, UserViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }

            var user = await _userService.UpdateUserAsync(id, viewModel);
            if (user == null)
            {
                ModelState.AddModelError("Email", "Email is already taken by another user.");
                return View(viewModel);
            }

            TempData["SuccessMessage"] = "User updated successfully!";
            return RedirectToAction(nameof(Details), new { id = user.Id });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var success = await _userService.DeleteUserAsync(id);
            if (success)
            {
                TempData["SuccessMessage"] = "User deleted successfully.";
            }
            else
            {
                TempData["ErrorMessage"] = "Unable to delete user.";
            }

            return RedirectToAction(nameof(Index));
        }
    }
}