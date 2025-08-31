using CarRentalSystemSeparation.Areas.Admin.Repositories;
using CarRentalSystemSeparation.Areas.Admin.Services;
using CarRentalSystemSeparation.Areas.Vehicle.Repositories;
using CarRentalSystemSeparation.Areas.Vehicle.Services;
using CarRentalSystemSeparation.Common.Data;
using CarRentalSystemSeparation.Common.Mapping;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Authentication.Cookies;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Add DbContext
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Add AutoMapper
//builder.Services.AddAutoMapper(typeof(AutoMapperProfile));
builder.Services.AddAutoMapper(cfg => cfg.AddProfile<AutoMapperProfile>());

// Register Repositories
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IVehicleRepository, VehicleRepository>();
builder.Services.AddScoped<IBannerRepository, BannerRepository>();
builder.Services.AddScoped<CarRentalSystemSeparation.Areas.Customer.Repositories.IBookingRepository, CarRentalSystemSeparation.Areas.Customer.Repositories.BookingRepository>();
builder.Services.AddScoped<CarRentalSystemSeparation.Areas.Booking.Repositories.IRentalRepository, CarRentalSystemSeparation.Areas.Booking.Repositories.RentalRepository>();

// Register Services
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IVehicleService, VehicleService>();
builder.Services.AddScoped<IBannerService, BannerService>();
builder.Services.AddScoped<CarRentalSystemSeparation.Areas.Customer.Services.IBookingService, CarRentalSystemSeparation.Areas.Customer.Services.BookingService>();
builder.Services.AddScoped<CarRentalSystemSeparation.Areas.Booking.Services.IRentalService, CarRentalSystemSeparation.Areas.Booking.Services.RentalService>();

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Account/Login";
        options.AccessDeniedPath = "/Account/AccessDenied";
    });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

// Area routing - {area:exists} first
app.MapControllerRoute(
    name: "areas",
    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

// Default route for guest/common views
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();