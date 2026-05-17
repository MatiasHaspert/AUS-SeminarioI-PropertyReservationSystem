namespace Application.DTOs.Reservation
{
    public class ReservationRequestDTO
    {
        public int PropertyId { get; set; }
        public DateOnly StartDate { get; set; }
        public DateOnly EndDate { get; set; }
        public int TotalGuests { get; set; }
    }
}
