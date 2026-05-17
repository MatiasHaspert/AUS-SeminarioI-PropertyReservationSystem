using Application.DTOs.PropertyAvailability;
using System.Net.Http.Json;

namespace WinFormsClient;

/// <summary>
/// Cliente para interactuar con los endpoints de disponibilidad de propiedades de la API.
/// </summary>
public class PropertyAvailabilityApiClient
{
    private readonly HttpClient _httpClient;

    public PropertyAvailabilityApiClient(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    /// <summary>
    /// Obtiene todas las disponibilidades de una propiedad.
    /// </summary>
    public async Task<IEnumerable<PropertyAvailabilityResponseDTO>?> GetPropertyAvailabilitiesAsync(int propertyId)
    {
        var response = await _httpClient.GetAsync($"api/propertyavailability/{propertyId}");
        
        if (response.IsSuccessStatusCode)
        {
            return await response.Content.ReadFromJsonAsync<IEnumerable<PropertyAvailabilityResponseDTO>>();
        }

        return null;
    }

    /// <summary>
    /// Crea una nueva disponibilidad para una propiedad.
    /// </summary>
    public async Task<PropertyAvailabilityResponseDTO?> CreatePropertyAvailabilityAsync(PropertyAvailabilityRequestDTO availabilityDto)
    {
        var response = await _httpClient.PostAsJsonAsync("api/propertyavailability", availabilityDto);
        
        if (response.IsSuccessStatusCode)
        {
            return await response.Content.ReadFromJsonAsync<PropertyAvailabilityResponseDTO>();
        }

        return null;
    }

    /// <summary>
    /// Actualiza una disponibilidad existente.
    /// </summary>
    public async Task<bool> UpdatePropertyAvailabilityAsync(int id, PropertyAvailabilityRequestDTO availabilityDto)
    {
        var response = await _httpClient.PutAsJsonAsync($"api/propertyavailability/{id}", availabilityDto);
        return response.IsSuccessStatusCode;
    }

    /// <summary>
    /// Elimina una disponibilidad.
    /// </summary>
    public async Task<bool> DeletePropertyAvailabilityAsync(int id)
    {
        var response = await _httpClient.DeleteAsync($"api/propertyavailability/{id}");
        return response.IsSuccessStatusCode;
    }
}
