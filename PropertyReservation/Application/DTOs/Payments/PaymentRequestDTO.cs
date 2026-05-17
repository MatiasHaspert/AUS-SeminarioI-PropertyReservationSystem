using Domain.Enums;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Payments
{
    public class PaymentRequestDTO
    {
        [Required(ErrorMessage = "La reserva es obligatoria.")]
        public int reservationId { get; set; }
        [Required(ErrorMessage = "El método de pago es obligatorio.")]
        public PaymentMethod PaymentMethod { get; set; }
        [Required(ErrorMessage = "El comprobante es obligatorio.")]
        public IFormFile file { get; set; } = null!;
    }
}
