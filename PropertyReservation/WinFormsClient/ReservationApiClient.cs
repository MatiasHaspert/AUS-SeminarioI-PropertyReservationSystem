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
    /// CU-05: listado global para Administrador con filtros opcionales.
    /// </summary>
    public async Task<IEnumerable<ReservationResponseDTO>?> GetAllForAdminAsync(
        string? status = null,
        int? propertyId = null,
        int? guestId = null,
        DateOnly? from = null,
        DateOnly? to = null)
    {
        var parts = new List<string>();
        if (!string.IsNullOrWhiteSpace(status)) parts.Add($"status={Uri.EscapeDataString(status)}");
        if (propertyId.HasValue) parts.Add($"propertyId={propertyId}");
        if (guestId.HasValue) parts.Add($"guestId={guestId}");
        if (from.HasValue) parts.Add($"from={from:yyyy-MM-dd}");
        if (to.HasValue) parts.Add($"to={to:yyyy-MM-dd}");

        var url = "api/reservation/admin" + (parts.Count > 0 ? "?" + string.Join("&", parts) : string.Empty);
        var response = await _httpClient.GetAsync(url);
        if (response.IsSuccessStatusCode)
            return await response.Content.ReadFromJsonAsync<IEnumerable<ReservationResponseDTO>>();

        return null;
    }

    /// <summary>
    /// Cambia el estado de una reserva (transición sobre la máquina de estados).
    /// </summary>
    public async Task<bool> ChangeReservationStatusAsync(int id, ChangeReservationStatusDTO dto)
    {
        var response = await _httpClient.PatchAsJsonAsync($"api/reservation/{id}/status", dto);
        if (response.IsSuccessStatusCode) return true;

        var body = await response.Content.ReadAsStringAsync();
        var message = string.IsNullOrWhiteSpace(body) ? response.ReasonPhrase ?? "Error" : body;
        throw new HttpRequestException($"Error {(int)response.StatusCode}: {message}");
    }

    /// <summary>
    /// Actualiza el estado de una reserva (DTO legado).
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
