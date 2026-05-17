using AutoMapper;
using Infrastructure.Repositories;
using Application.DTOs.Amenity;
using Domain.Entities;

namespace Application.Services
{
    public class AmenityService
    {
        private readonly AmenityRepository _amenityRepository;
        private readonly IMapper _mapper;

        public AmenityService(AmenityRepository amenityRepository, IMapper mapper)
        {
            _amenityRepository = amenityRepository;
            _mapper = mapper;
        }

        public async Task<ICollection<AmenityResponseDTO>> GetAllAmenitiesAsync()
        {
            var amenities = await _amenityRepository.GetAllAsync();
            return amenities.Select(a => _mapper.Map<AmenityResponseDTO>(a)).ToList();
        }
        
        public async Task<AmenityResponseDTO> CreateAmenityAsync(AmenityRequestDTO amenityRequestDTO)
        {
            var amenity = _mapper.Map<Amenity>(amenityRequestDTO);
            await _amenityRepository.AddAsync(amenity);
            return _mapper.Map<AmenityResponseDTO>(amenity);
        }
        
        public async Task UpdateAmenityAsync(int amenityId, AmenityRequestDTO amenityRequestDTO)
        {
            var amenityExists = await _amenityRepository.ExistsAsync(amenityId);
            if (!amenityExists)
            {
                throw new ArgumentException("Servicio no encontrado.");
            }
            var amenity = _mapper.Map<Amenity>(amenityRequestDTO);
            amenity.Id = amenityId;
            await _amenityRepository.UpdateAsync(amenity);
        }

        public async Task DeleteAmenityAsync(int amenityId)
        {
            var amenity = await _amenityRepository.GetByIdAsync(amenityId);
            if (amenity == null)
            {
                throw new ArgumentException("Servicio no encontrado.");
            }
            await _amenityRepository.DeleteAsync(amenity);
        }

    }
}
