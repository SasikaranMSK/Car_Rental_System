using CarRentalSystemSeparation.Common.Enums;

namespace CarRentalSystemSeparation.Areas.Admin.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Email { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty;
        public UserRole Role { get; set; } = UserRole.Customer;
        public bool IsActive { get; set; } = true;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; }

        public string FullName => $"{FirstName} {LastName}";

        internal bool VerifyPassword(string password)
        {
            using var sha256 = System.Security.Cryptography.SHA256.Create();
            var hashedInput = Convert.ToBase64String(sha256.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password)));
            return PasswordHash == hashedInput;
        }
    }
}