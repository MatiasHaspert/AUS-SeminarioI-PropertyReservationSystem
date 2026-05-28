# Casos de Uso — WinFormsApp (Rol Administrador)

Esta carpeta contiene la especificación de los **casos de uso del Administrador** del sistema **PropertyReservationSystem**, que serán implementados en la aplicación de escritorio **WinFormsApp**.

Los casos de uso están redactados siguiendo el **Estándar Textual de Cockburn — Plantilla Estándar**:

1. **Identificador y Nombre**
2. **Actor Principal**
3. **Precondiciones**
4. **Garantías de Éxito (Postcondiciones)**
5. **Escenario Principal de Éxito (Flujo Básico)**
6. **Flujos Alternativos / Extensiones**

Al pie de cada caso se incluye una **Nota de Implementación** (no forma parte del estándar Cockburn) que enlaza el caso con el código real: endpoints del WebApi involucrados, formularios del WinFormsApp afectados y endpoints pendientes de crear.

---

## Contexto

El sistema cuenta con tres aplicaciones:

- **WebApi (.NET 8)** — backend REST con JWT, autoriza por rol (`User`, `Owner`, `Admin`).
- **WebApp (React)** — frontend para roles `User` y `Owner`.
- **WinFormsApp** — cliente de escritorio **exclusivo para el Administrador** (validado en `LoginForm`).

El Administrador no participa en los flujos de reserva ni publicación: su responsabilidad es **moderar y mantener** el sistema (usuarios, propiedades, reservas, pagos, reseñas, amenities) y monitorear su estado general.

---

## Índice de casos de uso

| ID | Nombre | Backend |
|----|--------|---------|
| [CU-01](CU-01-iniciar-sesion-admin.md) | Iniciar sesión como Administrador | Existente |
| [CU-02](CU-02-cerrar-sesion.md) | Cerrar sesión | Existente |
| [CU-03](CU-03-gestionar-usuarios.md) | Gestionar Usuarios | Requiere nuevo `UsersController` |
| [CU-04](CU-04-moderar-propiedades.md) | Moderar Propiedades | Ampliar políticas (Admin sobre endpoints actuales) |
| [CU-05](CU-05-gestionar-reservas.md) | Gestionar Reservas | Requiere endpoint global de listado |
| [CU-06](CU-06-aprobar-rechazar-pagos.md) | Aprobar / Rechazar Pagos | Ampliar política a Admin |
| [CU-07](CU-07-moderar-resenas.md) | Moderar Reseñas | Requiere `DELETE` en `ReviewController` |
| [CU-08](CU-08-gestionar-amenities.md) | Gestionar Amenities | Requiere `POST/PUT/DELETE` en `AmenityController` |
| [CU-09](CU-09-ver-dashboard-estadisticas.md) | Ver Dashboard / Estadísticas | Requiere nuevo `AdminController` |

---

## Convenciones de notación

- **Pasos del flujo principal:** numeración decimal simple (`1.`, `2.`, `3.` …).
- **Extensiones:** se nombran como `<paso>a`, `<paso>b`, etc. Si una extensión a su vez tiene pasos, se numeran como `<paso>a-1`, `<paso>a-2`, … (notación Cockburn).
- **Actores:** se asume que el **Actor Principal** siempre es el **Administrador**, salvo indicación expresa. Otros actores se listan como **secundarios**.
- **Sistema:** se refiere al conjunto WinFormsApp + WebApi + Base de Datos, salvo que se aclare lo contrario.
