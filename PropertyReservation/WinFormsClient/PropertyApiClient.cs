using Application.DTOs;
using Application.DTOs.Property;
using System.Net.Http.Json;

namespace WinFormsClient;

/// <summary>
/// Cliente para interactuar con los endpoints de propiedades de la API.
/// </summary>
public class PropertyApiClient
{
    private readonly HttpClient _httpClient;

    public PropertyApiClient(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    /// <summary>
    /// Obtiene todas las propiedades. Si includeDeleted=true incluye las soft-deleted (solo Admin).
    /// </summary>
    public async Task<IEnumerable<PropertyListResponseDTO>?> GetPropertiesAsync(bool includeDeleted = false)
    {
        var url = "api/property" + (includeDeleted ? "?includeDeleted=true" : string.Empty);
        var response = await _httpClient.GetAsync(url);

        if (response.IsSuccessStatusCode)
        {
            return await response.Content.ReadFromJsonAsync<IEnumerable<PropertyListResponseDTO>>();
        }

        return null;
    }

    /// <summary>
    /// Obtiene una propiedad por su ID.
    /// </summary>
    public async Task<PropertyDetailsResponseDTO?> GetPropertyByIdAsync(int id)
    {
        var response = await _httpClient.GetAsync($"api/property/{id}");
        
        if (response.IsSuccessStatusCode)
        {
            return await response.Content.ReadFromJsonAsync<PropertyDetailsResponseDTO>();
        }

        return null;
    }

    /// <summary>
    /// Crea una nueva propiedad.
    /// </summary>
    public async Task<PropertyListResponseDTO?> CreatePropertyAsync(PropertyRequestDTO propertyDto)
    {
        var response = await _httpClient.PostAsJsonAsync("api/property", propertyDto);
        
        if (response.IsSuccessStatusCode)
        {
            return await response.Content.ReadFromJsonAsync<PropertyListResponseDTO>();
        }

        var errorContent = await response.Content.ReadAsStringAsync();
        var message = string.IsNullOrWhiteSpace(errorContent)
            ? response.ReasonPhrase ?? "Error desconocido al crear la propiedad."
            : errorContent;

        throw new HttpRequestException($"Error {(int)response.StatusCode} ({response.StatusCode}): {message}");
    }

    /// <summary>
    /// Actualiza una propiedad existente.
    /// </summary>
    public async Task<bool> UpdatePropertyAsync(int id, PropertyRequestDTO propertyDto)
    {
        var response = await _httpClient.PutAsJsonAsync($"api/property/{id}", propertyDto);
        if (response.IsSuccessStatusCode) return true;

        var body = await response.Content.ReadAsStringAsync();
        var message = string.IsNullOrWhiteSpace(body) ? response.ReasonPhrase ?? "Error" : body;
        throw new HttpRequestException($"Error {(int)response.StatusCode}: {message}");
    }

    /// <summary>
    /// CU-04: elimina la propiedad. Si hard=true se elimina físicamente (solo Admin).
    /// </summary>
    public async Task<bool> DeletePropertyAsync(int id, bool hard = false)
    {
        var url = $"api/property/{id}" + (hard ? "?hard=true" : string.Empty);
        var response = await _httpClient.DeleteAsync(url);
        if (response.IsSuccessStatusCode) return true;

        var body = await response.Content.ReadAsStringAsync();
        var message = string.IsNullOrWhiteSpace(body) ? response.ReasonPhrase ?? "Error" : body;
        throw new HttpRequestException($"Error {(int)response.StatusCode}: {message}");
    }

    /// <summary>
    /// CU-04: restaura una propiedad soft-deleted (Admin).
    /// </summary>
    public async Task<bool> RestorePropertyAsync(int id)
    {
        var response = await _httpClient.PostAsync($"api/property/{id}/restore", content: null);
        if (response.IsSuccessStatusCode) return true;

        var body = await response.Content.ReadAsStringAsync();
        var message = string.IsNullOrWhiteSpace(body) ? response.ReasonPhrase ?? "Error" : body;
        throw new HttpRequestException($"Error {(int)response.StatusCode}: {message}");
    }
}
