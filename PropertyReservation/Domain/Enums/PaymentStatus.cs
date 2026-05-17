using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Enums
{
    public enum PaymentStatus
    {
        UnderReview,
        Approved,
        Rejected
    }
}

/*
User crea la reserva -> Reserva creada con estado "Pending"
Owner revisa la reserva -> Si todo es correcto, cambia el estado a "PendingPayment", si hay algún problema, cambia el estado a "Cancelled"
User realiza el pago -> Si el pago es exitoso, cambia el estado a "Confirmed", si el pago falla, cambia el estado a "Cancelled",
Una vez que la reserva está "Confirmed", el usuario puede disfrutar de su experiencia. Después de la experiencia, el owner cambia el estado a "Completed".
*/