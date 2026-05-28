# CU-08: Gestionar Amenities

## 1. Identificador y Nombre
**CU-08 — Gestionar el catálogo de Amenities disponibles para asignar a propiedades**

## 2. Actor Principal
- **Administrador** con sesión iniciada.

Actores secundarios:
- **Owners** que asignan amenities a sus propiedades — reciben pasivamente el efecto del catálogo actualizado.

## 3. Precondiciones
- El Administrador completó el **CU-01**.
- La tabla `Amenities` existe (al menos con los 20 amenities sembrados inicialmente: Wi-Fi, Aire Acondicionado, Pileta, etc.).

## 4. Garantías de Éxito (Postcondiciones)
- El catálogo de amenities queda actualizado conforme a las altas, modificaciones y bajas realizadas.
- Las propiedades existentes que tenían asignado un amenity eliminado dejan de mostrarlo (la relación many-to-many `Property↔Amenity` se libera).
- No se permite eliminar un amenity referenciado por al menos una propiedad, salvo confirmación explícita que también libere la relación.

## 5. Escenario Principal de Éxito (Flujo Básico)
1. El Administrador selecciona **Gestionar Amenities** en el `DashboardForm`.
2. El sistema abre el `AmenityManagementForm` y solicita `GET /api/amenity` al WebApi.
3. El WebApi devuelve la lista completa de amenities (Id, Name).
4. El sistema muestra la grilla ordenada alfabéticamente.
5. El Administrador elige una acción: **Crear nuevo**, **Editar** o **Eliminar**.
6. El sistema muestra el formulario o la confirmación correspondiente.
7. El Administrador completa los datos / confirma.
8. El sistema envía la petición correspondiente (`POST`, `PUT` o `DELETE /api/amenity`).
9. El WebApi valida, persiste y devuelve el catálogo actualizado.
10. El sistema refresca la grilla y muestra un mensaje de éxito.

## 6. Flujos Alternativos / Extensiones

**5a. Acción "Crear nuevo"**
- 5a-1. El sistema muestra un cuadro con el campo *Nombre*.
- 5a-2. El Administrador ingresa el nombre y presiona **Guardar**.
- 5a-3. El sistema valida que el nombre no esté vacío ni duplicado.
- 5a-4. El sistema envía `POST /api/amenity` con `{ name }`.

**5b. Acción "Editar"**
- 5b-1. El sistema muestra el campo *Nombre* precargado con el valor actual.
- 5b-2. El Administrador modifica el texto y presiona **Guardar**.
- 5b-3. El sistema envía `PUT /api/amenity/{id}` con `{ name }`.

**5c. Acción "Eliminar"**
- 5c-1. El sistema verifica cuántas propiedades referencian al amenity.
- 5c-2. Si **cero propiedades** lo referencian, muestra confirmación simple.
- 5c-3. Si **al menos una** propiedad lo referencia, muestra el mensaje *"Este amenity está asignado a N propiedades. Si lo elimina, será removido de todas. ¿Confirma?"* y exige confirmación explícita.
- 5c-4. El sistema envía `DELETE /api/amenity/{id}`.

**7a. El nombre ingresado ya existe**
- 7a-1. El sistema muestra el mensaje *"Ya existe un amenity con ese nombre"* y vuelve al paso 5a-2 / 5b-2.

**7b. El Administrador cancela la operación**
- 7b-1. El sistema cierra el cuadro y vuelve al paso 5 sin cambios.

**9a. El WebApi devuelve un error**
- 9a-1. El sistema muestra el mensaje devuelto y conserva el estado actual de la grilla.

---

### Nota de Implementación
- **Endpoints WebApi:** En `WebApi/Controllers/AmenityController.cs`:
  - `GET /api/amenity` ✅ existe (público).
  - `POST /api/amenity` ⚠️ **pendiente de crear** — `[Authorize(Policy = "AdminOnly")]`.
  - `PUT /api/amenity/{id}` ⚠️ **pendiente de crear** — `[Authorize(Policy = "AdminOnly")]`.
  - `DELETE /api/amenity/{id}` ⚠️ **pendiente de crear** — `[Authorize(Policy = "AdminOnly")]`.
- **Servicio Application:** Ampliar `AmenityService` con `CreateAsync`, `UpdateAsync`, `DeleteAsync` usando el ya existente `AmenityRepository`.
- **DTOs:** `AmenityRequestDTO` ya existe — verificar y reutilizar.
- **Cliente WinForms:** Extender `AmenityApiClient` en `WinFormsClient/` con los métodos de escritura.
- **Forms:** ⚠️ Crear `AmenityManagementForm` en `WinFormsApp/` (grilla + diálogo de alta/edición).
- **Estado backend:** ⚠️ Requiere ampliar `AmenityController` con el CRUD completo.
