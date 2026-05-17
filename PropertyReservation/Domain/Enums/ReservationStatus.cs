namespace Domain.Enums
{
    public enum ReservationStatus
    {
        PendingPayment = 0,   // usuario eligió fechas, falta subir comprobante
        PaymentUploaded = 2,  // usuario subió comprobante
        Confirmed = 3,        // owner valida pago
        Rejected = 4,         // owner rechaza el pago
        Expired = 5,          // sistema expira
        Cancelled = 6,        // cancelación manual
        Completed = 7         // estadía finalizada
    }
}
