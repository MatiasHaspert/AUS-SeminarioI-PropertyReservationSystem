# CU-04: Moderar Propiedades

## 1. Identificador y Nombre
**CU-04 — Moderar Propiedades publicadas en el sistema**

## 2. Actor Principal
- **Administrador** con sesión iniciada.

Actores secundarios:
- **Owner** propietario de la propiedad afectada (no participa en el flujo, pero recibe el efecto de la moderación).

## 3. Precondiciones
- El Administrador completó el **CU-01**.
- Existe al menos una propiedad publicada (o eliminada lógicamente) en el sistema.

## 4. Garantías de Éxito (Postcondiciones)
- La propiedad queda con su nuevo estado persistido: modificada en sus campos, eliminada lógicamente (soft-delete) o eliminada definitivamente (hard-delete) según la acción.
- La lista mostrada en pantalla refleja el nuevo estado.
- Las reservas asociadas a una propiedad eliminada lógicamente conservan su historial.

## 5. Escenario Principal de Éxito (Flujo Básico)
1. El Administrador selecciona **Moderar Propiedades** en el `DashboardForm`.
2. El sistema abre el `PropertyListForm` y solicita la lista global al WebApi (`GET /api/property?includeDeleted=true`).
3. El WebApi devuelve todas las propiedades (incluyendo las marcadas como `IsDeleted = true`).
4. El sistema muestra la grilla con las columnas: Título, Owner, Precio por noche, Estado (Activa / Eliminada), Fecha de alta.
5. El Administrador filtra/ordena la grilla y selecciona una propiedad.
6. El Administrador elige una acción: **Ver detalle**, **Editar**, **Eliminar (soft)**, **Eliminar definitivamente** o **Restaurar** (si está eliminada).
7. El sistema solicita confirmación describiendo la acción y sus consecuencias.
8. El Administrador confirma.
9. El sistema envía la petición correspondiente al WebApi.
10. El WebApi valida la operación, la persiste y devuelve el estado actualizado.
11. El sistema refresca la grilla y muestra un mensaje de éxito.

## 6. Flujos Alternativos / Extensiones

**3a. No hay propiedades en el sistema**
- 3a-1. El sistema muestra el mensaje *"No hay propiedades registradas"* y deshabilita los botones de acción.

**6a. Acción "Editar"**
- 6a-1. El sistema abre `PropertyForm` en modo edición con los datos precargados.
- 6a-2. El Administrador modifica los campos permitidos (título, descripción, precio, capacidad, dirección, amenities).
- 6a-3. El Administrador presiona **Guardar** y el sistema envía `PUT /api/property/{id}`.
- 6a-4. Continúa en el paso 10 del flujo principal.

**6b. Acción "Eliminar (soft)"**
- 6b-1. El sistema envía `DELETE /api/property/{id}` (delete lógico — `IsDeleted = true`).
- 6b-2. Las reservas existentes se conservan; la propiedad deja de aparecer en búsquedas públicas.

**6c. Acción "Eliminar definitivamente"**
- 6c-1. El sistema verifica que la propiedad no tenga reservas activas (estados `Confirmed`, `PaymentUploaded`, `PendingPayment`).
- 6c-2. Si tiene reservas activas, muestra *"No se puede eliminar definitivamente: existen reservas activas. Cancele las reservas primero (CU-05)."* y cancela la acción.
- 6c-3. Si no tiene reservas activas, envía `DELETE /api/property/{id}?hard=true`.

**6d. Acción "Restaurar"**
- 6d-1. Disponible sólo si la propiedad está marcada como `IsDeleted`.
- 6d-2. El sistema envía `POST /api/property/{id}/restore`.

**8a. El Administrador cancela la confirmación**
- 8a-1. El sistema vuelve al paso 6 sin modificar nada.

**10a. El WebApi devuelve un error**
- 10a-1. El sistema muestra el mensaje devuelto y conserva el estado actual de la grilla.

---

### Nota de Implementación
- **Endpoints WebApi:** Parcialmente existentes en `WebApi/Controllers/PropertyController.cs`:
  - `GET /api/property` ✅ existe pero filtra soft-deleted — **ampliar con parámetro `?includeDeleted=true` accesible sólo para Admin**.
  - `PUT /api/property/{id}` ✅ existe pero restringido a Owner del recurso — **ampliar política para permitir Admin**.
  - `DELETE /api/property/{id}` ✅ existe (soft) — **agregar parámetro `?hard=true` accesible sólo para Admin**.
  - `POST /api/property/{id}/restore` ⚠️ **pendiente de crear**.
- **Cliente WinForms:** Extender `PropertyApiClient` en `WinFormsClient/` con los nuevos parámetros y endpoints.
- **Forms:** `WinFormsApp/PropertyListForm.cs` (extender: agregar filtro por estado, botones Eliminar definitivo y Restaurar) y `WinFormsApp/PropertyForm.cs` (habilitar modo edición — actualmente sólo crea).
- **Estado backend:** ⚠️ Ampliación de políticas + nuevos parámetros + un endpoint nuevo (`restore`).
