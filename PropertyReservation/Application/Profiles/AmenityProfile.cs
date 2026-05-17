using AutoMapper;
using Domain.Entities;
using Application.DTOs.Amenity;

namespace Application.Profiles
{
    public class AmenityProfile : Profile
    {
        public AmenityProfile()
        {
            CreateMap<Amenity, AmenityResponseDTO>();
            CreateMap<AmenityRequestDTO, Amenity>();
        }
    }
}
