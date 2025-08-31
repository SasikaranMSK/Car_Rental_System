using System.ComponentModel.DataAnnotations;

namespace CarRentalSystemSeparation.Areas.Customer.ViewModels
{
    public class BookingViewModel
    {
        [Required(ErrorMessage = "Vehicle selection is required")]
        public int VehicleId { get; set; }

        [Required(ErrorMessage = "Pickup date is required")]
        [DataType(DataType.Date)]
        [Display(Name = "Pickup Date")]
        public DateTime PickupDate { get; set; }

        [Required(ErrorMessage = "Return date is required")]
        [DataType(DataType.Date)]
        [Display(Name = "Return Date")]
        public DateTime ReturnDate { get; set; }

        [Required(ErrorMessage = "Pickup time is required")]
        [Display(Name = "Pickup Time")]
        public string PickupTime { get; set; } = string.Empty;

        [StringLength(500, ErrorMessage = "Notes cannot exceed 500 characters")]
        [Display(Name = "Additional Notes")]
        public string Notes { get; set; } = string.Empty;

        // Read-only properties for display
        public string VehicleName { get; set; } = string.Empty;
        public decimal PricePerDay { get; set; }
        public string VehicleImageUrl { get; set; } = string.Empty;
    }
}