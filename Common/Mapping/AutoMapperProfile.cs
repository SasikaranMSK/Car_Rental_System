using AutoMapper;
using CarRentalSystemSeparation.Areas.Admin.Models;
using CarRentalSystemSeparation.Areas.Admin.DTOs;
using CarRentalSystemSeparation.Areas.Admin.ViewModels;
using CarRentalSystemSeparation.Areas.Vehicle.Models;
using CarRentalSystemSeparation.Areas.Vehicle.DTOs;

namespace CarRentalSystemSeparation.Common.Mapping
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            // User mappings
            CreateMap<User, UserDTO>()
                .ForMember(dest => dest.PasswordHash, opt => opt.Ignore()); // Don't expose password hash

            CreateMap<UserViewModel, User>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore());

            // Vehicle mappings
            CreateMap<Vehicle, VehicleDTO>();
            CreateMap<Vehicle, VehicleListDTO>();

            // Banner mappings
            CreateMap<Banner, BannerDTO>();
        }
    }
}