# CU-09: Ver Dashboard / Estadísticas

## 1. Identificador y Nombre
**CU-09 — Ver el Dashboard con estadísticas generales del sistema**

## 2. Actor Principal
- **Administrador** con sesión iniciada.

## 3. Precondiciones
- El Administrador completó el **CU-01** y la aplicación se encuentra mostrando el `DashboardForm`.
- El servicio WebApi está accesible.

## 4. Garantías de Éxito (Postcondiciones)
- El Administrador visualiza en pantalla los indicadores clave del sistema actualizados al momento de la consulta:
  - Cantidad total de usuarios por rol (User / Owner / Admin).
  - Cantidad total de propiedades (activas y eliminadas lógicamente).
  - Cantidad de reservas por estado.
  - Cantidad de pagos pendientes de revisión.
  - Monto total facturado (suma de `TotalPrice` de reservas en estado `Completed`).
  - Top 5 propiedades más reservadas.
- No se produce ningún cambio de estado en el sistema (operación de sólo lectura).

## 5. Escenario Principal de Éxito (Flujo Básico)
1. Tras un inicio de sesión exitoso (CU-01), el sistema abre el `DashboardForm`.
2. El sistema solicita `GET /api/admin/stats` al WebApi.
3. El WebApi consulta los repositorios necesarios, arma el DTO de estadísticas y lo retorna.
4. El sistema renderiza los indicadores en tarjetas (KPIs), una tabla con el top 5 de propiedades y un acceso rápido a cada módulo (CU-03 a CU-08).
5. El Administrador visualiza la información y opcionalmente presiona **Actualizar** para reconsultar.

## 6. Flujos Alternativos / Extensiones

**2a. El WebApi no responde**
- 2a-1. El sistema muestra los KPIs como *"--"* y un banner *"No se pudieron cargar las estadísticas. Reintente."*.
- 2a-2. Los accesos rápidos a los demás CU permanecen funcionales.

**3a. El WebApi devuelve datos parciales (algún campo nulo o cero)**
- 3a-1. El sistema renderiza los valores disponibles y muestra *"--"* en los faltantes.

**5a. El Administrador presiona "Actualizar"**
- 5a-1. El caso de uso retorna al paso 2.

**5b. El Administrador presiona un acceso rápido (p. ej. "Pagos pendientes")**
- 5b-1. El sistema abre el formulario correspondiente, dando inicio al caso de uso asociado (CU-03, CU-04, CU-05, CU-06, CU-07 o CU-08).

---

### Nota de Implementación
- **Endpoints WebApi:** ⚠️ **Pendiente de crear**.
  - Crear `AdminController` con `GET /api/admin/stats` protegido por `[Authorize(Policy = "AdminOnly")]`.
- **DTO sugerido:** `AdminStatsDTO` con:
  - `UsersByRole: Dictionary<Role, int>`
  - `TotalProperties: int`, `DeletedProperties: int`
  - `ReservationsByStatus: Dictionary<ReservationStatus, int>`
  - `PendingPayments: int`
  - `TotalRevenue: decimal`
  - `TopProperties: List<{ PropertyId, Title, ReservationsCount }>`
- **Servicio Application:** Crear `AdminStatsService` que consulte agregaciones sobre `UserRepository`, `PropertyRepository`, `ReservationRepository`, `PaymentsRepository`.
- **Cliente WinForms:** ⚠️ Crear `AdminApiClient` en `WinFormsClient/` con `GetStatsAsync()`.
- **Forms:** ⚠️ Extender `WinFormsApp/DashboardForm.cs` (hoy vacío) con tarjetas KPI, grilla del top 5 y botones de acceso rápido a los demás formularios.
- **Estado backend:** ⚠️ Requiere creación completa del controller, servicio y DTO; no agrega nuevas entidades.
