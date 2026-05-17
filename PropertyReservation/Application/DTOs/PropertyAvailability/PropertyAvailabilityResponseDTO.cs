namespace Application.DTOs.PropertyAvailability
{
    public class PropertyAvailabilityResponseDTO
    {
        public int Id { get; set; }
        public DateOnly StartDate { get; set; }
        public DateOnly EndDate { get; set; }
        public int PropertyId { get; set; }
    }
}
