using System.ComponentModel.DataAnnotations;
using CarRentalSystemSeparation.Common.Enums;

namespace CarRentalSystemSeparation.Areas.Vehicle.ViewModels
{
    public class VehicleViewModel
    {
        [Required(ErrorMessage = "Make is required")]
        [StringLength(50, ErrorMessage = "Make cannot exceed 50 characters")]
        public string Make { get; set; } = string.Empty;

        [Required(ErrorMessage = "Model is required")]
        [StringLength(50, ErrorMessage = "Model cannot exceed 50 characters")]
        public string Model { get; set; } = string.Empty;

        [Required(ErrorMessage = "Year is required")]
        [Range(1900, 2030, ErrorMessage = "Year must be between 1900 and 2030")]
        public int Year { get; set; }

        [Required(ErrorMessage = "Vehicle type is required")]
        public VehicleType Type { get; set; }

        [Required(ErrorMessage = "Price per day is required")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Price must be greater than 0")]
        [Display(Name = "Price Per Day")]
        public decimal PricePerDay { get; set; }

        [Required(ErrorMessage = "Image URL is required")]
        [Url(ErrorMessage = "Please enter a valid URL")]
        [Display(Name = "Image URL")]
        public string ImageUrl { get; set; } = string.Empty;

        public VehicleStatus Status { get; set; } = VehicleStatus.Available;

        [StringLength(1000, ErrorMessage = "Description cannot exceed 1000 characters")]
        public string Description { get; set; } = string.Empty;

        [StringLength(500, ErrorMessage = "Features cannot exceed 500 characters")]
        public string Features { get; set; } = string.Empty;
    }
}