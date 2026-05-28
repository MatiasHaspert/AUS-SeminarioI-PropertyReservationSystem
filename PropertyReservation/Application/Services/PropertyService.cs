using AutoMapper;
using Infrastructure.Repositories;
using Application.DTOs;
using Application.DTOs.Property;
using Domain.Entities;

namespace Application.Services
{
    public class PropertyService
    {
        private readonly PropertyRepository _propertyRepository;
        private readonly AmenityRepository _amenityRepository;
        private readonly CurrentUserService _currentUserService;
        private readonly IMapper _mapper;

        public PropertyService(
            PropertyRepository propertyRepository,
            AmenityRepository amenityRepository,
            CurrentUserService currentUserService,
            IMapper mapper
        )
        {
            _propertyRepository = propertyRepository;
            _amenityRepository = amenityRepository;
            _mapper = mapper;
            _currentUserService = currentUserService;
        }

        public async Task<PropertyDetailsResponseDTO> GetPropertyByIdAsync(int id)
        {
            var property = await _propertyRepository.GetByIdAsync(id);
            return _mapper.Map<PropertyDetailsResponseDTO>(property);
        }

        public async Task<IEnumerable<PropertyListResponseDTO>> GetPropertiesAsync()
        {
            var properties = await _propertyRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<PropertyListResponseDTO>>(properties);
        }

        public async Task<PropertyListResponseDTO> CreatePropertyAsync(PropertyRequestDTO propertyDTO)
        {
            var ownerId = _currentUserService.UserId;
            
            Property property = _mapper.Map<Property>(propertyDTO);

            property.OwnerId = ownerId;

            if (propertyDTO.AmenityIds.Any())
            {
                property.Amenities = await _amenityRepository.GetByIdsAsync(propertyDTO.AmenityIds);
            }

            await _propertyRepository.AddAsync(property);
            return _mapper.Map<PropertyListResponseDTO>(property);
        }

        public async Task<IEnumerable<PropertyListResponseDTO>> SearchPropertiesAsync(PropertySearchRequestDTO request)
        {
            var properties = await _propertyRepository.SearchAsync(
                request.City,
                request.Guests,
                request.CheckIn,
                request.CheckOut
            );
            return _mapper.Map<IEnumerable<PropertyListResponseDTO>>(properties);
        }

        public async Task PutPropertyAsync(int id, PropertyRequestDTO propertyDTO)
        {
            var ownerId = _currentUserService.UserId;

            var property = await _propertyRepository.GetByIdWithAmenitiesAsync(id);

            // Validar que la propiedad exista
            if (property == null)
                throw new KeyNotFoundException("Propiedad no encontrada.");

            // Validar que el usuario actual sea el dueño de la propiedad
            if (property.OwnerId != ownerId)
                throw new UnauthorizedAccessException("No tienes permiso para modificar esta propiedad.");

            _mapper.Map(propertyDTO, property);

            // Actualizar amenities solo si se envían IDs (regla actual: "si hay alguno" se reemplaza, caso contrario se preserva)
            if (propertyDTO.AmenityIds.Any())
            {
                property.Amenities = await _amenityRepository.GetByIdsAsync(propertyDTO.AmenityIds);
            }
            await _propertyRepository.UpdateAsync(property);
        }

        public async Task<IEnumerable<PropertyListResponseDTO>> GetPropertiesByCurrentOwnerAsync()
        {
            var ownerId = _currentUserService.UserId;

            var properties = await _propertyRepository.GetByOwnerIdAsync(ownerId);

            return _mapper.Map<IEnumerable<PropertyListResponseDTO>>(properties);
        }

        public async Task DeleteSafePropertyAsync(int propertyId)
        {
            var ownerId = _currentUserService.UserId;

            // Validar que la propiedad exista
            var property = await _propertyRepository.GetByIdWithReservationsAsync(propertyId);
            if (property == null)
                throw new KeyNotFoundException("Propiedad no encontrada.");

            // Validar que el usuario actual sea el dueño de la propiedad
            if (property.OwnerId != ownerId)
                throw new UnauthorizedAccessException("No tienes permiso para eliminar esta propiedad.");

            // Validar que no tenga reservas futuras
            var now = DateOnly.FromDateTime(DateTime.Now);
            if (property.Reservations.Any(r => r.EndDate >= now))
                throw new InvalidOperationException("No se puede eliminar la propiedad porque tiene reservas futuras.");

            property.IsDeleted = true;

            await _propertyRepository.UpdateAsync(property);
        }

        // MAQUETA — CU-04
        public Task<IEnumerable<PropertyListResponseDTO>> GetAllPropertiesIncludingDeletedAsync()
        {
            // TODO: usar PropertyRepository con .IgnoreQueryFilters() para incluir IsDeleted=true.
            throw new NotImplementedException("CU-04: listar todas las propiedades incluyendo las soft-deleted.");
        }

        // MAQUETA — CU-04
        public Task HardDeletePropertyAsync(int propertyId)
        {
            // TODO: validar que la propiedad no tenga reservas activas
            // (estados Confirmed, PaymentUploaded, PendingPayment) y luego eliminarla físicamente.
            throw new NotImplementedException("CU-04: hard-delete de propiedad (Admin).");
        }

        // MAQUETA — CU-04
        public Task RestorePropertyAsync(int propertyId)
        {
            // TODO: localizar la propiedad con .IgnoreQueryFilters(),
            // verificar que IsDeleted=true y restaurar (IsDeleted=false).
            throw new NotImplementedException("CU-04: restaurar propiedad soft-deleted (Admin).");
        }
    }
}
