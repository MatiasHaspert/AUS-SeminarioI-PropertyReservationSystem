using AutoMapper;
using Application.DTOs.Reservation;
using Domain.Entities;

namespace Application.Profiles
{
    public class ReservationProfile : Profile
    {
        public ReservationProfile()
        {
            CreateMap<Reservation, ReservationResponseDTO>()
                .ForMember(dest => dest.PropertyTitle, opt => opt.MapFrom(src => src.Property.Title))
                .ForMember(dest => dest.PropertyImageUrl, opt => opt.MapFrom(src => src.Property.Images.FirstOrDefault(img => img.IsMainImage).Url))
                .ForMember(dest => dest.GuestName, opt => opt.MapFrom(src => (src.Guest.Name + " " + src.Guest.LastName).Trim()));
            CreateMap<ReservationRequestDTO, Reservation>();
            CreateMap<Reservation, ReservationPublicDTO>();
            CreateMap<Reservation, MyReservationResponseDTO>()
                .ForMember(dest => dest.PropertyTitle, opt => opt.MapFrom(src => src.Property.Title))
                .ForMember(dest => dest.PropertyImageUrl, opt => opt.MapFrom(src => src.Property.Images.FirstOrDefault(img => img.IsMainImage).Url));
        }
    }
}
