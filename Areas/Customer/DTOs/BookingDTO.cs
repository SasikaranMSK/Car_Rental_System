using CarRentalSystemSeparation.Common.Enums;

namespace CarRentalSystemSeparation.Areas.Customer.DTOs
{
    public class BookingDTO
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int VehicleId { get; set; }
        public string VehicleName { get; set; } = string.Empty;
        public string VehicleImageUrl { get; set; } = string.Empty;
        public DateTime PickupDate { get; set; }
        public DateTime ReturnDate { get; set; }
        public string PickupTime { get; set; } = string.Empty;
        public decimal TotalAmount { get; set; }
        public BookingStatus Status { get; set; }
        public string Notes { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
        public int TotalDays { get; set; }
        public string UserName { get; set; } = string.Empty;
    }

    public class BookingListDTO
    {
        public int Id { get; set; }
        public string VehicleName { get; set; } = string.Empty;
        public string VehicleImageUrl { get; set; } = string.Empty;
        public DateTime PickupDate { get; set; }
        public DateTime ReturnDate { get; set; }
        public decimal TotalAmount { get; set; }
        public BookingStatus Status { get; set; }
        public int TotalDays { get; set; }
    }
}