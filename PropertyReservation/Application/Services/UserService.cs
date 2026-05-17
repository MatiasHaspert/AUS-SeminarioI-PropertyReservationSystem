using Application.DTOs.User;
using AutoMapper;
using Infrastructure.Repositories;
using Domain.Entities;

namespace Application.Services
{
    public class UserService
    {
        private readonly UserRepository _userRepository;
        private readonly IMapper _mapper;

        public UserService(UserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<UserResponseDTO?> GetUserByIdAsync(int userId)
        {
            var user = await _userRepository.GetByIdAsync(userId);
            return user != null ? _mapper.Map<UserResponseDTO>(user) : null;
        }

    }
}
