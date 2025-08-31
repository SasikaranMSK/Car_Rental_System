using System.ComponentModel.DataAnnotations;
using CarRentalSystemSeparation.Common.Enums;

namespace CarRentalSystemSeparation.Areas.Booking.ViewModels
{
    public class RentalViewModel
    {
        [Required(ErrorMessage = "Booking ID is required")]
        public int BookingId { get; set; }

        [Required(ErrorMessage = "Actual pickup date is required")]
        [DataType(DataType.DateTime)]
        [Display(Name = "Actual Pickup Date")]
        public DateTime ActualPickupDate { get; set; }

        [DataType(DataType.DateTime)]
        [Display(Name = "Actual Return Date")]
        public DateTime? ActualReturnDate { get; set; }

        [Required(ErrorMessage = "Actual amount is required")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Amount must be greater than 0")]
        [Display(Name = "Actual Amount")]
        public decimal ActualAmount { get; set; }

        [StringLength(500, ErrorMessage = "Pickup notes cannot exceed 500 characters")]
        [Display(Name = "Pickup Notes")]
        public string PickupNotes { get; set; } = string.Empty;

        [StringLength(500, ErrorMessage = "Return notes cannot exceed 500 characters")]
        [Display(Name = "Return Notes")]
        public string ReturnNotes { get; set; } = string.Empty;

        public RentalStatus Status { get; set; } = RentalStatus.Active;

        // Read-only properties for display
        public string VehicleName { get; set; } = string.Empty;
        public string CustomerName { get; set; } = string.Empty;
        public decimal EstimatedAmount { get; set; }
    }
}