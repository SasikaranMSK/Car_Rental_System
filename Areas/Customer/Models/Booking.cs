using CarRentalSystemSeparation.Common.Enums;

namespace CarRentalSystemSeparation.Areas.Customer.Models
{
    public class Booking
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int VehicleId { get; set; }
        public DateTime PickupDate { get; set; }
        public DateTime ReturnDate { get; set; }
        public string PickupTime { get; set; } = string.Empty;
        public decimal TotalAmount { get; set; }
        public BookingStatus Status { get; set; } = BookingStatus.Pending;
        public string Notes { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; }

        // Navigation properties
        public virtual Areas.Admin.Models.User User { get; set; } = null!;
        public virtual Areas.Vehicle.Models.Vehicle Vehicle { get; set; } = null!;

        public int TotalDays => (ReturnDate.Date - PickupDate.Date).Days + 1;
    }
}