using CarRentalSystemSeparation.Common.Enums;

namespace CarRentalSystemSeparation.Areas.Booking.Models
{
    public class Rental
    {
        public int Id { get; set; }
        public int BookingId { get; set; }
        public DateTime ActualPickupDate { get; set; }
        public DateTime? ActualReturnDate { get; set; }
        public decimal ActualAmount { get; set; }
        public string PickupNotes { get; set; } = string.Empty;
        public string ReturnNotes { get; set; } = string.Empty;
        public RentalStatus Status { get; set; } = RentalStatus.Active;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; }

        // Navigation property
        public virtual Areas.Customer.Models.Booking Booking { get; set; } = null!;
    }
}