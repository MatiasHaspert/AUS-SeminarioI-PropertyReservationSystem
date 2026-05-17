using Application.DTOs.Reservation;
using System.Net.Http.Json;

namespace WinFormsClient;

/// <summary>
/// Cliente para interactuar con los endpoints de reservas de la API.
/// </summary>
public class ReservationApiClient
{
    private readonly HttpClient _httpClient;

    public ReservationApiClient(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    /// <summary>
    /// Obtiene una reserva por su ID.
    /// </summary>
    public async Task<ReservationResponseDTO?> GetReservationByIdAsync(int id)
    {
        var response = await _httpClient.GetAsync($"api/reservation/{id}");
        
        if (response.IsSuccessStatusCode)
        {
            return await response.Content.ReadFromJsonAsync<ReservationResponseDTO>();
        }

        return null;
    }

    /// <summary>
    /// Crea una nueva reserva.
    /// </summary>
    public async Task<ReservationResponseDTO?> CreateReservationAsync(ReservationRequestDTO reservationDto)
    {
        var response = await _httpClient.PostAsJsonAsync("api/reservation", reservationDto);
        
        if (response.IsSuccessStatusCode)
        {
            return await response.Content.ReadFromJsonAsync<ReservationResponseDTO>();
        }

        return null;
    }

    /// <summary>
    /// Actualiza una reserva existente.
    /// </summary>
    public async Task<bool> UpdateReservationAsync(int id, ReservationRequestDTO reservationDto)
    {
        var response = await _httpClient.PutAsJsonAsync($"api/reservation/{id}", reservationDto);
        return response.IsSuccessStatusCode;
    }

    /// <summary>
    /// Actualiza el estado de una reserva (para administradores).
    /// </summary>
    public async Task<bool> UpdateReservationStatusAsync(int id, UpdateReservationStatusDTO statusDto)
    {
        var response = await _httpClient.PatchAsJsonAsync($"api/reservation/{id}/status", statusDto);
        return response.IsSuccessStatusCode;
    }

    /// <summary>
    /// Cancela una reserva.
    /// </summary>
    public async Task<bool> DeleteReservationAsync(int id)
    {
        var response = await _httpClient.DeleteAsync($"api/reservation/{id}");
        return response.IsSuccessStatusCode;
    }
}
