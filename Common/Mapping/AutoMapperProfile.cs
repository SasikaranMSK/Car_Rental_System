using AutoMapper;
using CarRentalSystemSeparation.Areas.Admin.Models;
using CarRentalSystemSeparation.Areas.Admin.DTOs;
using CarRentalSystemSeparation.Areas.Admin.ViewModels;
using CarRentalSystemSeparation.Areas.Vehicle.Models;
using CarRentalSystemSeparation.Areas.Vehicle.DTOs;
using CarRentalSystemSeparation.Areas.Vehicle.ViewModels;
using CarRentalSystemSeparation.Areas.Customer.Models;
using CarRentalSystemSeparation.Areas.Customer.DTOs;
using CarRentalSystemSeparation.Areas.Customer.ViewModels;
using CarRentalSystemSeparation.Areas.Booking.Models;
using CarRentalSystemSeparation.Areas.Booking.DTOs;
using CarRentalSystemSeparation.Areas.Booking.ViewModels;

namespace CarRentalSystemSeparation.Common.Mapping
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            // User mappings
            CreateMap<User, UserDTO>();
                //.ForMember(dest => dest.PasswordHash, opt => opt.Ignore()); // Don't expose password hash

            CreateMap<UserViewModel, User>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore());

            // Vehicle mappings
            CreateMap<Vehicle, VehicleDTO>();
            CreateMap<Vehicle, VehicleListDTO>();
            CreateMap<VehicleViewModel, Vehicle>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore());

            // Banner mappings
            CreateMap<Banner, BannerDTO>();
            // .ForMember(dest => dest.PasswordHash, opt => opt.Ignore()); // Don't expose password hash

            // Booking mappings
            CreateMap<Booking, BookingDTO>()
                .ForMember(dest => dest.VehicleName, opt => opt.MapFrom(src => src.Vehicle.DisplayName))
                .ForMember(dest => dest.VehicleImageUrl, opt => opt.MapFrom(src => src.Vehicle.ImageUrl))
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.User.FullName));

            CreateMap<Booking, BookingListDTO>()
                .ForMember(dest => dest.VehicleName, opt => opt.MapFrom(src => src.Vehicle.DisplayName))
                .ForMember(dest => dest.VehicleImageUrl, opt => opt.MapFrom(src => src.Vehicle.ImageUrl));

            CreateMap<BookingViewModel, Booking>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.UserId, opt => opt.Ignore())
                .ForMember(dest => dest.TotalAmount, opt => opt.Ignore())
                .ForMember(dest => dest.Status, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.User, opt => opt.Ignore())
                .ForMember(dest => dest.Vehicle, opt => opt.Ignore());

            // Rental mappings
            CreateMap<Rental, RentalDTO>()
                .ForMember(dest => dest.VehicleName, opt => opt.MapFrom(src => src.Booking.Vehicle.DisplayName))
                .ForMember(dest => dest.CustomerName, opt => opt.MapFrom(src => src.Booking.User.FullName));

            CreateMap<Rental, RentalListDTO>()
                .ForMember(dest => dest.VehicleName, opt => opt.MapFrom(src => src.Booking.Vehicle.DisplayName))
                .ForMember(dest => dest.CustomerName, opt => opt.MapFrom(src => src.Booking.User.FullName));

            CreateMap<RentalViewModel, Rental>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.Booking, opt => opt.Ignore());
        }
    }
}