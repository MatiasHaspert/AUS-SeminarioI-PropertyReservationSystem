using Application.DTOs.Admin;
using System.Net.Http.Json;

namespace WinFormsClient;

/// <summary>
/// Cliente para los endpoints del Administrador (CU-09 — Dashboard / Estadísticas).
/// </summary>
public class AdminApiClient
{
    private readonly HttpClient _httpClient;

    public AdminApiClient(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    /// <summary>
    /// Obtiene el resumen de estadísticas globales del sistema.
    /// </summary>
    public async Task<AdminStatsDTO?> GetStatsAsync()
    {
        var response = await _httpClient.GetAsync("api/admin/stats");
        if (response.IsSuccessStatusCode)
            return await response.Content.ReadFromJsonAsync<AdminStatsDTO>();

        return null;
    }
}
