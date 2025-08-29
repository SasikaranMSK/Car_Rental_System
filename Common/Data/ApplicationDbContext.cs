using Microsoft.EntityFrameworkCore;
using CarRentalSystemSeparation.Areas.Admin.Models;
using CarRentalSystemSeparation.Areas.Vehicle.Models;

namespace CarRentalSystemSeparation.Common.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Areas.Vehicle.Models.Vehicle> Vehicles { get; set; }
        public DbSet<Banner> Banners { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // User configuration
            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Email).IsRequired().HasMaxLength(100);
                entity.Property(e => e.FirstName).IsRequired().HasMaxLength(50);
                entity.Property(e => e.LastName).IsRequired().HasMaxLength(50);
                entity.Property(e => e.PasswordHash).IsRequired();
                entity.HasIndex(e => e.Email).IsUnique();
            });

            // Vehicle configuration
            modelBuilder.Entity<Areas.Vehicle.Models.Vehicle>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Make).IsRequired().HasMaxLength(50);
                entity.Property(e => e.Model).IsRequired().HasMaxLength(50);
                entity.Property(e => e.Year).IsRequired();
                entity.Property(e => e.PricePerDay).HasColumnType("decimal(10,2)");
            });

            // Banner configuration
            modelBuilder.Entity<Banner>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Title).IsRequired().HasMaxLength(100);
                entity.Property(e => e.ImageUrl).IsRequired().HasMaxLength(500);
            });

            // Seed data
            SeedData(modelBuilder);
        }

        private void SeedData(ModelBuilder modelBuilder)
        {
            // Seed Banners
            modelBuilder.Entity<Banner>().HasData(
                new Banner
                {
                    Id = 1,
                    Title = "Premium Car Rental Experience",
                    Subtitle = "Discover our fleet of luxury vehicles",
                    ImageUrl = "https://images.pexels.com/photos/116675/pexels-photo-116675.jpeg?auto=compress&cs=tinysrgb&w=1200",
                    IsActive = true,
                    DisplayOrder = 1
                },
                new Banner
                {
                    Id = 2,
                    Title = "Best Deals on Car Rentals",
                    Subtitle = "Book now and save up to 30%",
                    ImageUrl = "https://images.pexels.com/photos/1545743/pexels-photo-1545743.jpeg?auto=compress&cs=tinysrgb&w=1200",
                    IsActive = true,
                    DisplayOrder = 2
                },
                new Banner
                {
                    Id = 3,
                    Title = "24/7 Customer Support",
                    Subtitle = "We're here to help you anytime",
                    ImageUrl = "https://images.pexels.com/photos/627678/pexels-photo-627678.jpeg?auto=compress&cs=tinysrgb&w=1200",
                    IsActive = true,
                    DisplayOrder = 3
                }
            );

            // Seed Vehicles
            modelBuilder.Entity<Areas.Vehicle.Models.Vehicle>().HasData(
                new Areas.Vehicle.Models.Vehicle
                {
                    Id = 1,
                    Make = "Toyota",
                    Model = "Camry",
                    Year = 2023,
                    Type = Common.Enums.VehicleType.Sedan,
                    PricePerDay = 65.00m,
                    ImageUrl = "https://images.pexels.com/photos/116675/pexels-photo-116675.jpeg?auto=compress&cs=tinysrgb&w=400",
                    Status = Common.Enums.VehicleStatus.Available,
                    Description = "Comfortable and reliable sedan perfect for business trips and daily commuting.",
                    Features = "GPS Navigation, Bluetooth, Air Conditioning, Automatic Transmission"
                },
                new Areas.Vehicle.Models.Vehicle
                {
                    Id = 2,
                    Make = "Honda",
                    Model = "CR-V",
                    Year = 2023,
                    Type = Common.Enums.VehicleType.SUV,
                    PricePerDay = 85.00m,
                    ImageUrl = "https://images.pexels.com/photos/1545743/pexels-photo-1545743.jpeg?auto=compress&cs=tinysrgb&w=400",
                    Status = Common.Enums.VehicleStatus.Available,
                    Description = "Spacious SUV ideal for family trips and outdoor adventures.",
                    Features = "All-Wheel Drive, Backup Camera, Heated Seats, Cargo Space"
                },
                new Areas.Vehicle.Models.Vehicle
                {
                    Id = 3,
                    Make = "BMW",
                    Model = "3 Series",
                    Year = 2024,
                    Type = Common.Enums.VehicleType.Sedan,
                    PricePerDay = 120.00m,
                    ImageUrl = "https://images.pexels.com/photos/627678/pexels-photo-627678.jpeg?auto=compress&cs=tinysrgb&w=400",
                    Status = Common.Enums.VehicleStatus.Available,
                    Description = "Luxury sedan with premium features and exceptional performance.",
                    Features = "Premium Sound System, Leather Interior, Sport Mode, Premium Navigation"
                },
                new Areas.Vehicle.Models.Vehicle
                {
                    Id = 4,
                    Make = "Ford",
                    Model = "Mustang",
                    Year = 2023,
                    Type = Common.Enums.VehicleType.Convertible,
                    PricePerDay = 150.00m,
                    ImageUrl = "https://images.pexels.com/photos/544542/pexels-photo-544542.jpeg?auto=compress&cs=tinysrgb&w=400",
                    Status = Common.Enums.VehicleStatus.Available,
                    Description = "Iconic convertible sports car for an unforgettable driving experience.",
                    Features = "V8 Engine, Convertible Top, Sport Exhaust, Premium Audio"
                },
                new Areas.Vehicle.Models.Vehicle
                {
                    Id = 5,
                    Make = "Chevrolet",
                    Model = "Tahoe",
                    Year = 2023,
                    Type = Common.Enums.VehicleType.SUV,
                    PricePerDay = 95.00m,
                    ImageUrl = "https://images.pexels.com/photos/1638459/pexels-photo-1638459.jpeg?auto=compress&cs=tinysrgb&w=400",
                    Status = Common.Enums.VehicleStatus.Available,
                    Description = "Large SUV perfect for group travel and cargo hauling.",
                    Features = "Third Row Seating, Towing Capacity, Entertainment System, Climate Control"
                },
                new Areas.Vehicle.Models.Vehicle
                {
                    Id = 6,
                    Make = "Tesla",
                    Model = "Model S",
                    Year = 2024,
                    Type = Common.Enums.VehicleType.Sedan,
                    PricePerDay = 180.00m,
                    ImageUrl = "https://images.pexels.com/photos/919073/pexels-photo-919073.jpeg?auto=compress&cs=tinysrgb&w=400",
                    Status = Common.Enums.VehicleStatus.Available,
                    Description = "Electric luxury sedan with cutting-edge technology and zero emissions.",
                    Features = "Autopilot, Supercharging, Premium Interior, Over-the-air Updates"
                }
            );
        }

       
    }
}