namespace CarRentalSystemSeparation.Common.Enums
{
    public enum UserRole
    {
        Guest = 0,
        Customer = 1,
        Admin = 2,
        SuperAdmin = 3
    }

    public enum VehicleStatus
    {
        Available = 0,
        Rented = 1,
        Maintenance = 2,
        OutOfService = 3
    }

    public enum VehicleType
    {
        Sedan = 0,
        SUV = 1,
        Hatchback = 2,
        Convertible = 3,
        Truck = 4,
        Van = 5
    }
}