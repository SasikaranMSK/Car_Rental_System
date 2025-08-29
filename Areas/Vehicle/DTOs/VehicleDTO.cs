using CarRentalSystemSeparation.Common.Enums;

namespace CarRentalSystemSeparation.Areas.Vehicle.DTOs
{
    public class VehicleDTO
    {
        public int Id { get; set; }
        public string Make { get; set; } = string.Empty;
        public string Model { get; set; } = string.Empty;
        public int Year { get; set; }
        public VehicleType Type { get; set; }
        public decimal PricePerDay { get; set; }
        public string ImageUrl { get; set; } = string.Empty;
        public VehicleStatus Status { get; set; }
        public string Description { get; set; } = string.Empty;
        public string Features { get; set; } = string.Empty;
        public string DisplayName { get; set; } = string.Empty;
    }

    public class VehicleListDTO
    {
        public int Id { get; set; }
        public string Make { get; set; } = string.Empty;
        public string Model { get; set; } = string.Empty;
        public int Year { get; set; }
        public VehicleType Type { get; set; }
        public decimal PricePerDay { get; set; }
        public string ImageUrl { get; set; } = string.Empty;
        public VehicleStatus Status { get; set; }
        public string DisplayName { get; set; } = string.Empty;
    }

    public class BannerDTO
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Subtitle { get; set; } = string.Empty;
        public string ImageUrl { get; set; } = string.Empty;
        public bool IsActive { get; set; }
        public int DisplayOrder { get; set; }
    }
}