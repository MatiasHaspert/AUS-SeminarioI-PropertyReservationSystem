# CU-05: Gestionar Reservas

## 1. Identificador y Nombre
**CU-05 — Gestionar Reservas del sistema**

## 2. Actor Principal
- **Administrador** con sesión iniciada.

Actores secundarios:
- **Huésped** (User) y **Owner** asociados a la reserva — reciben el efecto del cambio de estado o cancelación, pero no participan en el flujo.

## 3. Precondiciones
- El Administrador completó el **CU-01**.
- Existe al menos una reserva en cualquier estado dentro del sistema.

## 4. Garantías de Éxito (Postcondiciones)
- El estado de la reserva afectada queda persistido conforme a la transición elegida y respeta la máquina de estados definida en `Domain/Entities/Reservation.cs` (`PendingPayment` → `PaymentUploaded` → `Confirmed` → `Completed`; con ramas `Rejected`, `Cancelled`, `Expired`).
- La grilla refleja el nuevo estado.
- Si la reserva tiene un pago asociado, los efectos colaterales sobre el `Payment` quedan consistentes (ver CU-06).

## 5. Escenario Principal de Éxito (Flujo Básico)
1. El Administrador selecciona **Gestionar Reservas** en el `DashboardForm`.
2. El sistema abre el `ReservationManagementForm` y solicita `GET /api/reservation/admin` al WebApi.
3. El WebApi devuelve todas las reservas del sistema con sus datos relevantes (Id, Propiedad, Huésped, Fechas, Estado, Monto total).
4. El sistema muestra la grilla y permite filtrar por estado, propiedad, huésped y rango de fechas.
5. El Administrador selecciona una reserva y elige una acción: **Ver detalle**, **Cambiar estado** o **Cancelar**.
6. El sistema valida que la transición de estado solicitada sea compatible con el estado actual.
7. El sistema solicita confirmación al Administrador, indicando el estado origen y destino.
8. El Administrador confirma.
9. El sistema envía `PATCH /api/reservation/{id}/status` con el nuevo estado.
10. El WebApi aplica la transición sobre la entidad, persiste y devuelve la reserva actualizada.
11. El sistema refresca la fila en la grilla y muestra un mensaje de éxito.

## 6. Flujos Alternativos / Extensiones

**3a. No hay reservas registradas**
- 3a-1. El sistema muestra *"No hay reservas en el sistema"* y deshabilita los botones de acción.

**5a. Acción "Ver detalle"**
- 5a-1. El sistema abre una vista con datos completos de la reserva: huésped, propiedad, fechas, huéspedes totales, monto, estado, pago asociado (si existe) y enlace al comprobante.
- 5a-2. El caso de uso termina aquí.

**5b. Acción "Cancelar"**
- 5b-1. Sólo aplicable a reservas en estado `PendingPayment`, `PaymentUploaded` o `Confirmed`.
- 5b-2. El sistema envía `PATCH /api/reservation/{id}/status` con destino `Cancelled`.

**6a. Transición de estado inválida**
- 6a-1. El sistema muestra el mensaje *"La transición {origen}→{destino} no es válida"* y cancela la acción.
- 6a-2. Retorna al paso 5.

**8a. El Administrador cancela la confirmación**
- 8a-1. El sistema vuelve al paso 5 sin modificar nada.

**10a. El WebApi devuelve un error**
- 10a-1. El sistema muestra el mensaje devuelto por el servidor y conserva el estado actual de la grilla.
- 10a-2. Retorna al paso 5.

---

### Nota de Implementación
- **Endpoints WebApi:** En `WebApi/Controllers/ReservationController.cs`:
  - `GET /api/reservation/admin` ⚠️ **pendiente de crear** — listado global (los actuales `by-property/{id}` y `owner` están acotados al Owner).
  - `GET /api/reservation/{id}` ✅ existe — ampliar política para Admin.
  - `PATCH /api/reservation/{id}/status` ✅ existe — ampliar política para Admin.
- **Máquina de estados:** Reutilizar los métodos ya definidos en `Domain/Entities/Reservation.cs`: `Reject()`, `Cancel()`, `Completed()`, `ConfirmPayment()`, `ReturnToPendingPaymentAfterRejected()`.
- **Cliente WinForms:** Extender `ReservationApiClient` en `WinFormsClient/` con el método `GetAllAsync()` para el endpoint admin.
- **Forms:** ⚠️ Crear `ReservationManagementForm` en `WinFormsApp/`.
- **Estado backend:** ⚠️ Requiere un endpoint nuevo (`/admin`) y ampliación de políticas en endpoints existentes.
