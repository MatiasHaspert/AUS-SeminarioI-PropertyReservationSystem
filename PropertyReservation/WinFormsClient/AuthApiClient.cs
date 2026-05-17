using Application.DTOs.User;
using System.Net.Http.Json;

namespace WinFormsClient;

/// <summary>
/// Cliente para interactuar con los endpoints de autenticación de la API.
/// </summary>
public class AuthApiClient
{
    private readonly HttpClient _httpClient;

    public AuthApiClient(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    /// <summary>
    /// Inicia sesión con las credenciales proporcionadas.
    /// </summary>
    /// <param name="loginDto">DTO con las credenciales de inicio de sesión.</param>
    /// <returns>El DTO de respuesta con el token JWT y datos del usuario.</returns>
    public async Task<LoginResponseDTO?> LoginAsync(UserLoginDTO loginDto)
    {
        var response = await _httpClient.PostAsJsonAsync("api/auth/login", loginDto);

        if (response.IsSuccessStatusCode)
        {
            var loginResponse = await response.Content.ReadFromJsonAsync<LoginResponseDTO>();
            if (loginResponse != null)
            {
                SessionManager.JwtToken = loginResponse.Token;
            }
            return loginResponse;
        }

        return null;
    }

    /// <summary>
    /// Registra un nuevo usuario.
    /// </summary>
    /// <param name="registerDto">DTO con los datos de registro.</param>
    /// <returns>El DTO de respuesta con el token JWT y datos del usuario.</returns>
    public async Task<LoginResponseDTO?> RegisterAsync(UserRegisterDTO registerDto)
    {
        var response = await _httpClient.PostAsJsonAsync("api/auth/register", registerDto);

        if (response.IsSuccessStatusCode)
        {
            var loginResponse = await response.Content.ReadFromJsonAsync<LoginResponseDTO>();
            if (loginResponse != null)
            {
                SessionManager.JwtToken = loginResponse.Token;
            }
            return loginResponse;
        }

        return null;
    }

    /// <summary>
    /// Cierra la sesión del usuario actual.
    /// </summary>
    public void Logout()
    {
        SessionManager.Logout();
    }
}
