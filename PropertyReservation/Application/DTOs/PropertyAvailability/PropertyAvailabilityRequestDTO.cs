using System;
using System.ComponentModel.DataAnnotations;

namespace Application.DTOs.PropertyAvailability
{
    public class PropertyAvailabilityRequestDTO
    {
        [Required(ErrorMessage = "La fecha de inicio es obligatoria.")]
        public DateOnly StartDate { get; set; }

        [Required(ErrorMessage = "La fecha de fin es obligatoria.")]
        public DateOnly EndDate { get; set; }

        [Required(ErrorMessage = "El ID de la propiedad es obligatorio.")]
        [Range(1, int.MaxValue, ErrorMessage = "El ID de la propiedad debe ser válido.")]
        public int PropertyId { get; set; }
    }
}
