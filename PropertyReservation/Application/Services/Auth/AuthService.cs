using Application.DTOs.User;
using AutoMapper;
using Domain.Entities;
using Domain.Enums;
using Infrastructure.Repositories;
using System.Security.Claims;

namespace Application.Services.Auth
{
    // Servicio de autenticación que maneja registro y login de usuarios.
    public class AuthService
    {
        private readonly UserRepository _userRepository;
        private readonly PasswordService _passwordService;
        private readonly JwtTokenGeneratorService _jwtTokenGeneratorService;
        private readonly IMapper _mapper;
        private readonly CurrentUserService currentUserService;

        public AuthService(
            UserRepository userRepository,
            PasswordService passwordService,
            JwtTokenGeneratorService jwtTokenGeneratorService,
            IMapper mapper,
            CurrentUserService currentUserService)
        {
            _userRepository = userRepository;
            _passwordService = passwordService;
            _jwtTokenGeneratorService = jwtTokenGeneratorService;
            _mapper = mapper;
            this.currentUserService = currentUserService;
        }

        /// Registra un nuevo usuario en el sistema.
        public async Task<UserResponseDTO> RegisterAsync(UserRegisterDTO registerDto)
        {
            // Verificar si el email ya existe
            if (await _userRepository.ExistsWithEmailAsync(registerDto.Email))
            {
                throw new InvalidOperationException($"El email '{registerDto.Email}' ya está en uso.");
            }

            // Hashear la contraseña
            var hashedPassword = _passwordService.HashPassword(registerDto.Password);

            // Crear el nuevo usuario
            var user = new User
            {
                Name = registerDto.Name,
                LastName = registerDto.LastName,
                Email = registerDto.Email,
                PasswordHash = hashedPassword,
                Phone = registerDto.Phone ?? string.Empty,
                Role = Role.User // Por defecto es User
            };

            await _userRepository.AddAsync(user);

            return _mapper.Map<UserResponseDTO>(user);
        }


        /// Autentica un usuario y genera un token JWT.
        public async Task<LoginResponseDTO> LoginAsync(UserLoginDTO loginDto)
        {
            // Buscar usuario por email
            var user = await _userRepository.GetByEmailAsync(loginDto.Email);

            if (user == null)
            {
                throw new UnauthorizedAccessException("Email o contraseña incorrectos.");
            }

            // Verificar la contraseña
            if (!_passwordService.VerifyPassword(user.PasswordHash, loginDto.Password))
            {
                throw new UnauthorizedAccessException("Email o contraseña incorrectos.");
            }

            // Generar el token JWT
            var token = _jwtTokenGeneratorService.GenerateToken(user);

            return new LoginResponseDTO
            {
                Token = token,
                User = _mapper.Map<UserResponseDTO>(user)
            };
        }

        public async Task<UserResponseDTO> GetCurrentUserAsync()
        {
            var userId = currentUserService.UserId;
            var user = await _userRepository.GetByIdAsync(userId);
            if (user == null)
                throw new UnauthorizedAccessException("Usuario no autenticado.");
            
            return _mapper.Map<UserResponseDTO>(user);
        }
    }
}
