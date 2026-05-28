using Application.DTOs.User;
using AutoMapper;
using Infrastructure.Repositories;
using Domain.Entities;

namespace Application.Services
{
    public class UserManagementService
    {
        private readonly UserRepository _userRepository;
        private readonly CurrentUserService _currentUser;
        private readonly IMapper _mapper;

        public UserManagementService(
            UserRepository userRepository,
            CurrentUserService currentUser,
            IMapper mapper)
        {
            _userRepository = userRepository;
            _currentUser = currentUser;
            _mapper = mapper;
        }

        public async Task<IEnumerable<UserListDTO>> GetAllAsync(string? emailFilter, string? roleFilter, bool? isActiveFilter)
        {
            IEnumerable<User> users = await _userRepository.GetByEmailOrRolOrActive(emailFilter, roleFilter, isActiveFilter);
            
            return _mapper.Map<IEnumerable<UserListDTO>>(users);
        }

        public Task<UserDetailDTO> GetByIdAsync(int userId)
        {
            throw new NotImplementedException("CU-03: detalle de usuario + estadísticas (reservas / propiedades).");
        }

        public Task UpdateRoleAsync(int userId, UpdateUserRoleDTO dto)
        {
            // Validar:
            //  - El usuario existe (KeyNotFoundException)
            //  - No es el propio Administrador autenticado (InvalidOperationException)
            //  - El rol nuevo es distinto del actual
            throw new NotImplementedException("CU-03: cambiar el rol del usuario.");
        }

        public Task UpdateStatusAsync(int userId, UpdateUserStatusDTO dto)
        {
            // Validar que el Admin no se deshabilite a sí mismo
            // Requiere agregar la propiedad IsActive en Domain/Entities/User.cs
            throw new NotImplementedException("CU-03: habilitar / deshabilitar usuario.");
        }

        public Task DeleteAsync(int userId)
        {
            // Eliminación lógica. Validar que no se elimine a sí mismo.
            throw new NotImplementedException("CU-03: baja lógica del usuario.");
        }
    }
}
