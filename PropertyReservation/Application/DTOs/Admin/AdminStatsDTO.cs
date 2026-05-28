namespace Application.DTOs.Admin
{
    public class AdminStatsDTO
    {
        // Cantidad total de usuarios por rol (User / Owner / Admin)
        public Dictionary<string, int> UsersByRole { get; set; } = new();

        public int TotalProperties { get; set; }
        public int TotalReviews { get; set; }

        // Cantidad de reservas por estado (clave = nombre del ReservationStatus)
        public Dictionary<string, int> ReservationsByStatus { get; set; } = new();

        public int PendingPayments { get; set; }
        public decimal TotalRevenue { get; set; }

        public List<TopPropertyDTO> TopProperties { get; set; } = new();
    }

    public class TopPropertyDTO
    {
        public int PropertyId { get; set; }
        public string Title { get; set; } = string.Empty;
        public int ReservationsCount { get; set; }
    }
}
