namespace Application.DTOs.User
{
    public class LoginResponseDTO
    {
        public string Token { get; set; } = string.Empty;
        public UserResponseDTO User { get; set; } = null!;
    }
}
