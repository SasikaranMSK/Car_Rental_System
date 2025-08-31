using CarRentalSystemSeparation.Common.Enums;

namespace CarRentalSystemSeparation.Areas.Booking.DTOs
{
    public class RentalDTO
    {
        public int Id { get; set; }
        public int BookingId { get; set; }
        public string VehicleName { get; set; } = string.Empty;
        public string CustomerName { get; set; } = string.Empty;
        public DateTime ActualPickupDate { get; set; }
        public DateTime? ActualReturnDate { get; set; }
        public decimal ActualAmount { get; set; }
        public string PickupNotes { get; set; } = string.Empty;
        public string ReturnNotes { get; set; } = string.Empty;
        public RentalStatus Status { get; set; }
        public DateTime CreatedAt { get; set; }
    }

    public class RentalListDTO
    {
        public int Id { get; set; }
        public int BookingId { get; set; }
        public string VehicleName { get; set; } = string.Empty;
        public string CustomerName { get; set; } = string.Empty;
        public DateTime ActualPickupDate { get; set; }
        public DateTime? ActualReturnDate { get; set; }
        public RentalStatus Status { get; set; }
        public decimal ActualAmount { get; set; }
    }
}