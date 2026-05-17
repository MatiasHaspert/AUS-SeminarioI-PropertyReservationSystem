namespace Application.DTOs
{
    public class PropertySearchRequestDTO
    {
        public string? City { get; set; }
        public int? Guests { get; set; }
        public DateOnly? CheckIn { get; set; }
        public DateOnly? CheckOut { get; set; }
    }
}
