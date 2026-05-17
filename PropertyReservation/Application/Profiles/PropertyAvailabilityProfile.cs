using AutoMapper;
using Domain.Entities;
using Application.DTOs.PropertyAvailability;

namespace Application.Profiles
{
    public class PropertyAvailabilityProfile : Profile
    {
        public PropertyAvailabilityProfile()
        {
            CreateMap<PropertyAvailability, PropertyAvailabilityResponseDTO>();
            CreateMap<PropertyAvailabilityRequestDTO, PropertyAvailability>();
            CreateMap<PropertyAvailability, PropertyAvailabilityPublicDTO>();
        }
    }
}
