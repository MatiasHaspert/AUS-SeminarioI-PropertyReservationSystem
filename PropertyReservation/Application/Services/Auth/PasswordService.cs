
namespace Application.Services.Auth
{
    /// Implementación del servicio de hash de contraseñas usando BCrypt.
    public class PasswordService
    {
   
        /// Genera un hash seguro de la contraseña usando BCrypt.
        public string HashPassword(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password);
        }

        /// Verifica si una contraseña coincide con un hash.
        public bool VerifyPassword(string hashedPassword, string providedPassword)
        {
            return BCrypt.Net.BCrypt.Verify(providedPassword, hashedPassword);
        }
    }
}