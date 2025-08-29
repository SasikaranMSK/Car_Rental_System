using AutoMapper;
using CarRentalSystemSeparation.Areas.Admin.Models;
using CarRentalSystemSeparation.Areas.Admin.DTOs;
using CarRentalSystemSeparation.Areas.Admin.ViewModels;
using CarRentalSystemSeparation.Areas.Admin.Repositories;

namespace CarRentalSystemSeparation.Areas.Admin.Services
{
    public interface IUserService
    {
        Task<IEnumerable<UserDTO>> GetAllUsersAsync();
        Task<UserDTO?> GetUserByIdAsync(int id);
        Task<UserDTO?> CreateUserAsync(UserViewModel viewModel);
        Task<UserDTO?> UpdateUserAsync(int id, UserViewModel viewModel);
        Task<bool> DeleteUserAsync(int id);
    }

    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public UserService(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<UserDTO>> GetAllUsersAsync()
        {
            var users = await _userRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<UserDTO>>(users);
        }

        public async Task<UserDTO?> GetUserByIdAsync(int id)
        {
            var user = await _userRepository.GetByIdAsync(id);
            if (user == null)
                return null;

            return _mapper.Map<UserDTO>(user);
        }

        public async Task<UserDTO?> CreateUserAsync(UserViewModel viewModel)
        {
            // Business rule: Check if email already exists
            var existingUser = await _userRepository.GetByEmailAsync(viewModel.Email);
            if (existingUser != null)
                return null; // Email already exists

            var user = _mapper.Map<User>(viewModel);
            
            // Business rule: Hash password (simplified for demo)
            user.PasswordHash = HashPassword(viewModel.Password);

            var createdUser = await _userRepository.CreateAsync(user);
            return _mapper.Map<UserDTO>(createdUser);
        }

        public async Task<UserDTO?> UpdateUserAsync(int id, UserViewModel viewModel)
        {
            var existingUser = await _userRepository.GetByIdAsync(id);
            if (existingUser == null)
                return null;

            // Business rule: Check if email is taken by another user
            var userWithEmail = await _userRepository.GetByEmailAsync(viewModel.Email);
            if (userWithEmail != null && userWithEmail.Id != id)
                return null;

            // Update user properties
            existingUser.Email = viewModel.Email;
            existingUser.FirstName = viewModel.FirstName;
            existingUser.LastName = viewModel.LastName;
            existingUser.Role = viewModel.Role;
            existingUser.IsActive = viewModel.IsActive;

            if (!string.IsNullOrWhiteSpace(viewModel.Password))
            {
                existingUser.PasswordHash = HashPassword(viewModel.Password);
            }

            var updatedUser = await _userRepository.UpdateAsync(existingUser);
            return _mapper.Map<UserDTO>(updatedUser);
        }

        public async Task<bool> DeleteUserAsync(int id)
        {
            return await _userRepository.DeleteAsync(id);
        }

        private string HashPassword(string password)
        {
            // Simplified password hashing for demo purposes
            // In production, use BCrypt or ASP.NET Core Identity
            return Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(password + "_salt"));
        }
    }
}