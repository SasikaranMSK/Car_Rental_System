using CarRentalSystemSeparation.Common.Enums;

namespace CarRentalSystemSeparation.Areas.Vehicle.Models
{
    public class Vehicle
    {
        public int Id { get; set; }
        public string Make { get; set; } = string.Empty;
        public string Model { get; set; } = string.Empty;
        public int Year { get; set; } 
        public VehicleType Type { get; set; }
        public decimal PricePerDay { get; set; }
        public string ImageUrl { get; set; } = string.Empty;
        public VehicleStatus Status { get; set; } = VehicleStatus.Available;
        public string Description { get; set; } = string.Empty;
        public string Features { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; }

        public string DisplayName => $"{Year} {Make} {Model}";
    }

    public class Banner
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Subtitle { get; set; } = string.Empty;
        public string ImageUrl { get; set; } = string.Empty;
        public bool IsActive { get; set; } = true;
        public int DisplayOrder { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}