using Application.DTOs.User;
using AutoMapper;
using Domain.Entities;

namespace Application.Profiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<User, UserResponseDTO>()
                .ForMember(
                    dest => dest.Role,
                    opt => opt.MapFrom(src => src.Role.ToString())
                );
            CreateMap<UserRegisterDTO, User>()
                .ForMember(dest => dest.PasswordHash, opt => opt.Ignore()); // Password se maneja en el servicio
        }
    }
}
