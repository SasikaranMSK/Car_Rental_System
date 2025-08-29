using CarRentalSystemSeparation.Common.Enums;

namespace CarRentalSystemSeparation.Areas.Admin.DTOs
{
    public class UserDTO
    {
        public int Id { get; set; }
        public string Email { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public UserRole Role { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public string FullName { get; set; } = string.Empty;
        public object PasswordHash { get; internal set; }
    }
}