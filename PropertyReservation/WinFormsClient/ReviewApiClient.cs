using Application.DTOs.Review;
using System.Net.Http.Json;

namespace WinFormsClient;

/// <summary>
/// Cliente para interactuar con los endpoints de reseñas de la API.
/// </summary>
public class ReviewApiClient
{
    private readonly HttpClient _httpClient;

    public ReviewApiClient(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    /// <summary>
    /// Obtiene todas las reseñas de una propiedad.
    /// </summary>
    public async Task<IEnumerable<ReviewResponseDTO>?> GetPropertyReviewsAsync(int propertyId)
    {
        var response = await _httpClient.GetAsync($"api/review?propertyId={propertyId}");
        
        if (response.IsSuccessStatusCode)
        {
            return await response.Content.ReadFromJsonAsync<IEnumerable<ReviewResponseDTO>>();
        }

        return null;
    }

    /// <summary>
    /// Obtiene una reseña por su ID.
    /// </summary>
    public async Task<ReviewResponseDTO?> GetReviewByIdAsync(int id)
    {
        var response = await _httpClient.GetAsync($"api/review/{id}");
        
        if (response.IsSuccessStatusCode)
        {
            return await response.Content.ReadFromJsonAsync<ReviewResponseDTO>();
        }

        return null;
    }

    /// <summary>
    /// Crea una nueva reseña.
    /// </summary>
    public async Task<ReviewResponseDTO?> CreateReviewAsync(ReviewRequestDTO reviewDto)
    {
        var response = await _httpClient.PostAsJsonAsync("api/review", reviewDto);
        
        if (response.IsSuccessStatusCode)
        {
            return await response.Content.ReadFromJsonAsync<ReviewResponseDTO>();
        }

        return null;
    }

    /// <summary>
    /// Actualiza una reseña existente.
    /// </summary>
    public async Task<bool> UpdateReviewAsync(int id, ReviewRequestDTO reviewDto)
    {
        var response = await _httpClient.PutAsJsonAsync($"api/review/{id}", reviewDto);
        return response.IsSuccessStatusCode;
    }

    /// <summary>
    /// Elimina una reseña.
    /// </summary>
    public async Task<bool> DeleteReviewAsync(int id)
    {
        var response = await _httpClient.DeleteAsync($"api/review/{id}");
        return response.IsSuccessStatusCode;
    }
}
