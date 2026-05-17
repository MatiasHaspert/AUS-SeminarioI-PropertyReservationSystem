using Domain.Enums;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Payment
    {
        [Key]
        public Guid Id { get; set; }
        public int ReservationId { get; set; }
        public Reservation Reservation { get; set; } = null!;
        public decimal Amount { get; set; }
        public DateTime PaymentDate { get; set; }
        public PaymentMethod Method { get; set; }
        public PaymentStatus Status { get; set; } = PaymentStatus.UnderReview;
        // Comprobante, ruta logica (no publica)
        public string ProofPath { get; set; } = string.Empty;
        public Payment() { }
        public Payment(
            Reservation reservation,
            PaymentMethod method
        )
        {
            Id = Guid.NewGuid();
            ReservationId = reservation.Id;
            Amount = reservation.TotalPrice; 
            PaymentDate = DateTime.UtcNow;
            Status = PaymentStatus.UnderReview;
            Method = method;
        }

        private void EnsureUnderReview()
        {
            if (Status != PaymentStatus.UnderReview)
                throw new InvalidOperationException(
                    "El pago ya fue procesado.");
        }

        public void Approve(Reservation reservation)
        {
            EnsureUnderReview();
            Status = PaymentStatus.Approved;
            reservation.ConfirmPayment();
        }

        public void Reject(Reservation reservation)
        {
            EnsureUnderReview();
            Status = PaymentStatus.Rejected;
            reservation.ReturnToPendingPaymentAfterRejected();
        }

    }
}
