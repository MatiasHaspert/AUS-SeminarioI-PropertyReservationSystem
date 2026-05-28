# CU-03: Gestionar Usuarios

## 1. Identificador y Nombre
**CU-03 — Gestionar Usuarios registrados en el sistema**

## 2. Actor Principal
- **Administrador** con sesión iniciada.

Actores secundarios:
- **Usuarios afectados** (User / Owner): reciben de manera indirecta el efecto del cambio de rol o deshabilitación, pero no participan en el caso de uso.

## 3. Precondiciones
- El Administrador completó el **CU-01** y se encuentra en el `DashboardForm`.
- Existe al menos un usuario registrado distinto del propio Administrador.

## 4. Garantías de Éxito (Postcondiciones)
- Los cambios solicitados (cambio de rol, deshabilitación, eliminación lógica) quedan persistidos en la base de datos.
- La lista mostrada en pantalla se refresca y refleja el nuevo estado.
- El propio Administrador no puede deshabilitarse ni eliminarse a sí mismo (invariante de seguridad).

## 5. Escenario Principal de Éxito (Flujo Básico)
1. El Administrador selecciona **Gestionar Usuarios** en el `DashboardForm`.
2. El sistema abre el formulario `UserManagementForm` y solicita `GET /api/users` al WebApi.
3. El WebApi devuelve la lista completa de usuarios con sus campos relevantes (Id, Email, Phone, Role, fecha de alta, estado).
4. El sistema muestra la lista en una grilla, ordenada alfabéticamente por email.
5. El Administrador opcionalmente filtra la lista por email, rol o estado.
6. El Administrador selecciona un usuario y elige una acción: **Cambiar rol**, **Deshabilitar**, **Habilitar** o **Ver detalle**.
7. El sistema muestra un cuadro de confirmación con el resumen de la acción.
8. El Administrador confirma la operación.
9. El sistema envía la petición correspondiente al WebApi (`PUT /api/users/{id}/role` o `PATCH /api/users/{id}/status`).
10. El WebApi valida la operación, la persiste y devuelve el estado actualizado.
11. El sistema refresca la fila en la grilla y muestra un mensaje de éxito.

## 6. Flujos Alternativos / Extensiones

**3a. La lista de usuarios está vacía**
- 3a-1. El sistema muestra el mensaje *"No hay usuarios registrados"* en el área de la grilla.
- 3a-2. Los botones de acción quedan deshabilitados.

**5a. El filtro no arroja resultados**
- 5a-1. El sistema muestra el mensaje *"Ningún usuario coincide con los criterios"* y mantiene los controles de filtro activos.

**6a. El Administrador selecciona "Cambiar rol"**
- 6a-1. El sistema muestra un combo con los valores válidos (`User`, `Owner`, `Admin`) excluyendo el rol actual.
- 6a-2. El Administrador elige el nuevo rol y continúa en el paso 7.

**6b. El Administrador selecciona "Deshabilitar"**
- 6b-1. El sistema valida que el usuario seleccionado **no sea el propio Administrador**.
- 6b-2. Si es el propio Administrador, muestra el mensaje *"No puede deshabilitarse a sí mismo"* y cancela la operación.

**6c. El Administrador selecciona "Ver detalle"**
- 6c-1. El sistema abre un cuadro con la información completa del usuario (datos personales, cantidad de reservas, propiedades publicadas si es Owner) y termina el caso de uso aquí.

**8a. El Administrador cancela la confirmación**
- 8a-1. El sistema cierra el cuadro de confirmación y vuelve al paso 6 sin realizar cambios.

**10a. El WebApi devuelve un error (HTTP 4xx/5xx)**
- 10a-1. El sistema muestra el mensaje devuelto por el servidor o un texto genérico *"No se pudo completar la operación"*.
- 10a-2. La grilla no se modifica y el caso de uso retorna al paso 6.

---

### Nota de Implementación
- **Endpoints WebApi:** ⚠️ **Pendientes de crear**. Se requiere un `UsersController` con:
  - `GET /api/users` (con paginación / filtro opcional).
  - `GET /api/users/{id}`.
  - `PUT /api/users/{id}/role` (body: `{ role: "User|Owner|Admin" }`).
  - `PATCH /api/users/{id}/status` o `DELETE /api/users/{id}` (eliminación lógica).
- **Servicio Application:** ⚠️ Pendiente — `UserManagementService` (reutilizar `UserRepository` ya existente en `Infrastructure/Repositories/UserRepository.cs`).
- **DTOs:** Crear `UserListDTO`, `UserDetailDTO`, `UpdateUserRoleDTO`.
- **Cliente WinForms:** ⚠️ Crear `UserApiClient` en `WinFormsClient/`.
- **Forms:** ⚠️ Crear `UserManagementForm` (grilla + filtros + botones de acción) en `WinFormsApp/`.
- **Política de autorización:** Todos los endpoints deben requerir `[Authorize(Policy = "AdminOnly")]`.
- **Estado backend:** ⚠️ Requiere creación completa (controller + service + DTOs).
