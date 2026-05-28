# CU-06: Aprobar / Rechazar Pagos

## 1. Identificador y Nombre
**CU-06 — Aprobar o Rechazar Pagos pendientes de revisión**

## 2. Actor Principal
- **Administrador** con sesión iniciada.

Actores secundarios:
- **Huésped** que cargó el comprobante — recibe el efecto sobre su reserva.
- **Owner** de la propiedad reservada — recibe la confirmación o rechazo del pago.

## 3. Precondiciones
- El Administrador completó el **CU-01**.
- Existe al menos un pago en estado `UnderReview` con su comprobante asociado.
- La reserva vinculada al pago está en estado `PaymentUploaded`.

## 4. Garantías de Éxito (Postcondiciones)
- El pago queda en estado `Approved` o `Rejected` según la decisión del Administrador.
- Si fue **aprobado**: la reserva asociada transiciona a `Confirmed`.
- Si fue **rechazado**: la reserva asociada vuelve a `PendingPayment` (`Reservation.ReturnToPendingPaymentAfterRejected()`).
- La lista de pagos bajo revisión se actualiza y deja de mostrar el pago procesado.

## 5. Escenario Principal de Éxito (Flujo Básico)
1. El Administrador selecciona **Aprobar / Rechazar Pagos** en el `DashboardForm`.
2. El sistema abre el `PaymentApprovalForm` y solicita `GET /api/payments/underReview` al WebApi.
3. El WebApi devuelve la lista de pagos en estado `UnderReview` con: Id, Propiedad, Huésped, Monto, Método (`Transfer`/`Cash`), Fecha del pago.
4. El sistema muestra la grilla ordenada por fecha del pago (más antigua primero).
5. El Administrador selecciona un pago y presiona **Ver comprobante**.
6. El sistema solicita `GET /api/payments/{id}/proof` y muestra el archivo (imagen o PDF) en un visor integrado.
7. El Administrador analiza el comprobante y selecciona una acción: **Aprobar** o **Rechazar**.
8. El sistema solicita confirmación, mostrando el resumen de la acción y, en caso de rechazo, pide un motivo opcional.
9. El Administrador confirma.
10. El sistema envía `PATCH /api/payments/{id}/status?status=Approved` o `?status=Rejected`.
11. El WebApi aplica `Payment.Approve(reservation)` o `Payment.Reject(reservation)`, persiste ambos cambios en una transacción y devuelve el pago actualizado.
12. El sistema remueve el pago de la grilla y muestra un mensaje de éxito.

## 6. Flujos Alternativos / Extensiones

**3a. No hay pagos bajo revisión**
- 3a-1. El sistema muestra el mensaje *"No hay pagos pendientes de revisión"* y deshabilita los botones de acción.

**6a. El comprobante no puede descargarse o está corrupto**
- 6a-1. El sistema muestra *"No se pudo cargar el comprobante. Reintente o rechace el pago."*.
- 6a-2. El Administrador puede continuar al paso 7 sólo con la opción **Rechazar** disponible.

**7a. Acción "Aprobar"**
- 7a-1. El sistema verifica que el monto del pago coincida con el `TotalPrice` de la reserva.
- 7a-2. Si no coincide, muestra advertencia *"El monto difiere del total de la reserva. ¿Aprobar igualmente?"* y exige doble confirmación.

**7b. Acción "Rechazar"**
- 7b-1. El sistema muestra un cuadro de texto para que el Administrador ingrese opcionalmente el motivo del rechazo.

**9a. El Administrador cancela la confirmación**
- 9a-1. El sistema vuelve al paso 7 sin modificar nada.

**11a. El WebApi devuelve un error**
- 11a-1. El sistema muestra el mensaje devuelto.
- 11a-2. El pago permanece visible en la grilla. Retorna al paso 5.

---

### Nota de Implementación
- **Endpoints WebApi:** En `WebApi/Controllers/PaymentsController.cs`:
  - `GET /api/payments/underReview` ✅ existe — **ampliar política**: hoy es Owner-only; permitir también a Admin (ver todos los pagos, no sólo los de sus propiedades).
  - `GET /api/payments/{id}/proof` ✅ existe — ampliar política a Admin.
  - `PATCH /api/payments/{id}/status?status={Approved|Rejected}` ✅ existe — ampliar política a Admin.
- **Lógica de dominio:** Los métodos `Payment.Approve(reservation)` y `Payment.Reject(reservation)` ya implementan correctamente las transiciones cruzadas Pago↔Reserva en `Domain/Entities/Payment.cs`.
- **Cliente WinForms:** `PaymentsApiClient` ya existe en `WinFormsClient/` — verificar que cubra los tres endpoints; agregar visor de comprobante (imagen / PDF).
- **Forms:** ⚠️ Crear `PaymentApprovalForm` en `WinFormsApp/` con grilla + visor de comprobante + botones Aprobar/Rechazar.
- **Estado backend:** ✅ Lógica completa; sólo falta ampliar la política de autorización para incluir Admin.
