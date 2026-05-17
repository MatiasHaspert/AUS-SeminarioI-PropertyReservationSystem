using Domain.Enums;

namespace Domain.Entities
{
    public class Reservation
    {
        public int Id { get; set; }
        public int PropertyId { get; set; }
        public Property Property { get; set; } = null!;
        public int GuestId { get; set; }
        public User Guest { get; set; } = null!;
        public DateOnly StartDate { get; set; }
        public DateOnly EndDate { get; set; }
        public int TotalGuests { get; set; }
        // Precio final calculado
        public decimal TotalPrice { get; set; }
        // Estado de la reserva
        public ReservationStatus Status { get; set; } = ReservationStatus.PendingPayment;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public ICollection<Payment> Payments { get; set; } = new List<Payment>();

        public bool IsPaid => Payments.Any(p => p.Status == PaymentStatus.Approved);

        private void EnsureStatus(params ReservationStatus[] allowed)
        {
            if (!allowed.Contains(Status))
                throw new InvalidOperationException(
                    $"Acción inválida desde estado {Status} de la reserva.");
        }

        public void Reject()
        {
            EnsureStatus(
                ReservationStatus.PendingPayment,
                ReservationStatus.PaymentUploaded
            );

            Status = ReservationStatus.Rejected;
        }

        public void UploadPayment()
        {
            EnsureStatus(ReservationStatus.PendingPayment);

            Status = ReservationStatus.PaymentUploaded;
        }

        public void ConfirmPayment()
        {
            EnsureStatus(ReservationStatus.PaymentUploaded);

            Status = ReservationStatus.Confirmed;
        }

        public void Completed()
        {
            EnsureStatus(ReservationStatus.Confirmed);

            Status = ReservationStatus.Completed;
        }

        public void Cancel()
        {
            EnsureStatus(
                ReservationStatus.PendingPayment,
                ReservationStatus.Confirmed
            );

            Status = ReservationStatus.Cancelled;
        }

        public void ReturnToPendingPaymentAfterRejected()
        {
            EnsureStatus(ReservationStatus.PaymentUploaded);
            Status = ReservationStatus.PendingPayment;
        }
    }
}
