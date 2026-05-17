using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace WinFormsClient;

/// <summary>
/// Gestor de sesión para mantener el estado de autenticación del usuario.
/// </summary>
public static class SessionManager
{
    private static string? _jwtToken;
    private static List<Claim> _claims = new();

    /// <summary>
    /// Obtiene o establece el token JWT. Al establecer un nuevo token,
    /// se decodifica automáticamente para extraer y almacenar los claims del usuario.
    /// </summary>
    public static string? JwtToken
    {
        get => _jwtToken;
        set
        {
            _jwtToken = value;
            _claims.Clear();

            if (!string.IsNullOrEmpty(_jwtToken))
            {
                var handler = new JwtSecurityTokenHandler();
                var token = handler.ReadJwtToken(_jwtToken);
                _claims.AddRange(token.Claims);
            }
        }
    }

    /// <summary>
    /// Indica si el usuario está actualmente autenticado.
    /// </summary>
    public static bool IsUserAuthenticated => !string.IsNullOrEmpty(JwtToken);

    /// <summary>
    /// Obtiene el rol del usuario autenticado desde los claims del token.
    /// </summary>
    public static string? GetUserRole() =>
        _claims.FirstOrDefault(c => c.Type == ClaimTypes.Role || c.Type == "role")?.Value;

    /// <summary>
    /// Obtiene el ID del usuario autenticado desde los claims del token.
    /// </summary>
    public static string? GetUserId() =>
        _claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier || c.Type == "sub")?.Value;

    /// <summary>
    /// Obtiene el email del usuario autenticado desde los claims del token.
    /// </summary>
    public static string? GetUserEmail() =>
        _claims.FirstOrDefault(c => c.Type == ClaimTypes.Email || c.Type == "email")?.Value;

    /// <summary>
    /// Cierra la sesión del usuario eliminando el token y los claims.
    /// </summary>
    public static void Logout()
    {
        _jwtToken = null;
        _claims.Clear();
    }
}
