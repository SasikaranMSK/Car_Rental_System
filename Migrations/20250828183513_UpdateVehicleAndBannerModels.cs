using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CarRentalSystemSeparation.Migrations
{
    /// <inheritdoc />
    public partial class UpdateVehicleAndBannerModels : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Banners",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Subtitle = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ImageUrl = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    DisplayOrder = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Banners", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Role = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Vehicles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Make = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Model = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Year = table.Column<int>(type: "int", nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false),
                    PricePerDay = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Features = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vehicles", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Banners",
                columns: new[] { "Id", "CreatedAt", "DisplayOrder", "ImageUrl", "IsActive", "Subtitle", "Title" },
                values: new object[,]
                {
                    { 1, new DateTime(2025, 8, 28, 18, 35, 11, 811, DateTimeKind.Utc).AddTicks(7447), 1, "https://images.pexels.com/photos/116675/pexels-photo-116675.jpeg?auto=compress&cs=tinysrgb&w=1200", true, "Discover our fleet of luxury vehicles", "Premium Car Rental Experience" },
                    { 2, new DateTime(2025, 8, 28, 18, 35, 11, 812, DateTimeKind.Utc).AddTicks(104), 2, "https://images.pexels.com/photos/1545743/pexels-photo-1545743.jpeg?auto=compress&cs=tinysrgb&w=1200", true, "Book now and save up to 30%", "Best Deals on Car Rentals" },
                    { 3, new DateTime(2025, 8, 28, 18, 35, 11, 812, DateTimeKind.Utc).AddTicks(109), 3, "https://images.pexels.com/photos/627678/pexels-photo-627678.jpeg?auto=compress&cs=tinysrgb&w=1200", true, "We're here to help you anytime", "24/7 Customer Support" }
                });

            migrationBuilder.InsertData(
                table: "Vehicles",
                columns: new[] { "Id", "CreatedAt", "Description", "Features", "ImageUrl", "Make", "Model", "PricePerDay", "Status", "Type", "UpdatedAt", "Year" },
                values: new object[,]
                {
                    { 1, new DateTime(2025, 8, 28, 18, 35, 11, 812, DateTimeKind.Utc).AddTicks(9961), "Comfortable and reliable sedan perfect for business trips and daily commuting.", "GPS Navigation, Bluetooth, Air Conditioning, Automatic Transmission", "https://images.pexels.com/photos/116675/pexels-photo-116675.jpeg?auto=compress&cs=tinysrgb&w=400", "Toyota", "Camry", 65.00m, 0, 0, null, 2023 },
                    { 2, new DateTime(2025, 8, 28, 18, 35, 11, 813, DateTimeKind.Utc).AddTicks(4113), "Spacious SUV ideal for family trips and outdoor adventures.", "All-Wheel Drive, Backup Camera, Heated Seats, Cargo Space", "https://images.pexels.com/photos/1545743/pexels-photo-1545743.jpeg?auto=compress&cs=tinysrgb&w=400", "Honda", "CR-V", 85.00m, 0, 1, null, 2023 },
                    { 3, new DateTime(2025, 8, 28, 18, 35, 11, 813, DateTimeKind.Utc).AddTicks(4119), "Luxury sedan with premium features and exceptional performance.", "Premium Sound System, Leather Interior, Sport Mode, Premium Navigation", "https://images.pexels.com/photos/627678/pexels-photo-627678.jpeg?auto=compress&cs=tinysrgb&w=400", "BMW", "3 Series", 120.00m, 0, 0, null, 2024 },
                    { 4, new DateTime(2025, 8, 28, 18, 35, 11, 813, DateTimeKind.Utc).AddTicks(4122), "Iconic convertible sports car for an unforgettable driving experience.", "V8 Engine, Convertible Top, Sport Exhaust, Premium Audio", "https://images.pexels.com/photos/544542/pexels-photo-544542.jpeg?auto=compress&cs=tinysrgb&w=400", "Ford", "Mustang", 150.00m, 0, 3, null, 2023 },
                    { 5, new DateTime(2025, 8, 28, 18, 35, 11, 813, DateTimeKind.Utc).AddTicks(4125), "Large SUV perfect for group travel and cargo hauling.", "Third Row Seating, Towing Capacity, Entertainment System, Climate Control", "https://images.pexels.com/photos/1638459/pexels-photo-1638459.jpeg?auto=compress&cs=tinysrgb&w=400", "Chevrolet", "Tahoe", 95.00m, 0, 1, null, 2023 },
                    { 6, new DateTime(2025, 8, 28, 18, 35, 11, 813, DateTimeKind.Utc).AddTicks(4128), "Electric luxury sedan with cutting-edge technology and zero emissions.", "Autopilot, Supercharging, Premium Interior, Over-the-air Updates", "https://images.pexels.com/photos/919073/pexels-photo-919073.jpeg?auto=compress&cs=tinysrgb&w=400", "Tesla", "Model S", 180.00m, 0, 0, null, 2024 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Users_Email",
                table: "Users",
                column: "Email",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Banners");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Vehicles");
        }
    }
}
