using Application.DTOs.Amenity;
using System.Net.Http.Json;

namespace WinFormsClient;

/// <summary>
/// Cliente para interactuar con los endpoints de amenidades de la API.
/// </summary>
public class AmenityApiClient
{
    private readonly HttpClient _httpClient;

    public AmenityApiClient(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    /// <summary>
    /// Obtiene todas las amenidades.
    /// </summary>
    public async Task<IEnumerable<AmenityResponseDTO>?> GetAllAmenitiesAsync()
    {
        var response = await _httpClient.GetAsync("api/amenity");
        
        if (response.IsSuccessStatusCode)
        {
            return await response.Content.ReadFromJsonAsync<IEnumerable<AmenityResponseDTO>>();
        }

        return null;
    }

    /// <summary>
    /// Crea una nueva amenidad.
    /// </summary>
    public async Task<AmenityResponseDTO?> CreateAmenityAsync(AmenityRequestDTO amenityDto)
    {
        var response = await _httpClient.PostAsJsonAsync("api/amenity", amenityDto);
        
        if (response.IsSuccessStatusCode)
        {
            return await response.Content.ReadFromJsonAsync<AmenityResponseDTO>();
        }

        return null;
    }

    /// <summary>
    /// Actualiza una amenidad existente.
    /// </summary>
    public async Task<bool> UpdateAmenityAsync(int id, AmenityRequestDTO amenityDto)
    {
        var response = await _httpClient.PutAsJsonAsync($"api/amenity/{id}", amenityDto);
        return response.IsSuccessStatusCode;
    }

    /// <summary>
    /// Elimina una amenidad.
    /// </summary>
    public async Task<bool> DeleteAmenityAsync(int id)
    {
        var response = await _httpClient.DeleteAsync($"api/amenity/{id}");
        return response.IsSuccessStatusCode;
    }
}
