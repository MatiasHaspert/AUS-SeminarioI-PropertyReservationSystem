# CU-07: Moderar Reseñas

## 1. Identificador y Nombre
**CU-07 — Moderar Reseñas (comentarios y calificaciones) sobre propiedades**

## 2. Actor Principal
- **Administrador** con sesión iniciada.

Actores secundarios:
- **User autor** de la reseña — sufre la eliminación de su comentario sin participar en el flujo.
- **Owner** de la propiedad reseñada — recibe el efecto sobre el promedio de calificaciones de su propiedad.

## 3. Precondiciones
- El Administrador completó el **CU-01**.
- Existe al menos una reseña en el sistema.

## 4. Garantías de Éxito (Postcondiciones)
- La reseña marcada como inapropiada queda eliminada de la base de datos.
- El promedio de calificación de la propiedad se recalcula automáticamente al consultarse.
- La lista mostrada al Administrador se refresca y deja de mostrar la reseña eliminada.

## 5. Escenario Principal de Éxito (Flujo Básico)
1. El Administrador selecciona **Moderar Reseñas** en el `DashboardForm`.
2. El sistema abre el `ReviewModerationForm` y muestra un selector de propiedad.
3. El Administrador elige una propiedad de la lista (o ingresa un filtro de búsqueda por título).
4. El sistema solicita `GET /api/review?propertyId={id}` al WebApi.
5. El WebApi devuelve la lista de reseñas de esa propiedad (Id, Autor, Rating, Comentario, Fecha).
6. El sistema muestra la grilla con las reseñas, ordenadas por fecha descendente.
7. El Administrador selecciona una reseña y presiona **Eliminar reseña**.
8. El sistema muestra un cuadro de confirmación con el texto completo de la reseña.
9. El Administrador confirma la eliminación.
10. El sistema envía `DELETE /api/review/{id}` al WebApi.
11. El WebApi elimina la reseña y devuelve confirmación.
12. El sistema remueve la fila de la grilla y muestra un mensaje de éxito.

## 6. Flujos Alternativos / Extensiones

**3a. El Administrador no selecciona ninguna propiedad**
- 3a-1. Los botones de acción permanecen deshabilitados hasta que se elija una propiedad.

**5a. La propiedad no tiene reseñas**
- 5a-1. El sistema muestra el mensaje *"Esta propiedad aún no tiene reseñas"* en el área de la grilla.

**7a. Acción "Ver detalle"**
- 7a-1. El sistema abre un cuadro con el texto completo de la reseña, datos del autor (email), fecha y la calificación.
- 7a-2. El caso de uso termina aquí si no se sigue con eliminación.

**9a. El Administrador cancela la confirmación**
- 9a-1. El sistema cierra el cuadro y vuelve al paso 7 sin cambios.

**11a. El WebApi devuelve un error**
- 11a-1. El sistema muestra el mensaje devuelto.
- 11a-2. La reseña permanece visible en la grilla. Retorna al paso 7.

---

### Nota de Implementación
- **Endpoints WebApi:** En `WebApi/Controllers/ReviewController.cs`:
  - `GET /api/review?propertyId={id}` ✅ existe (público).
  - `GET /api/review/{id}` ✅ existe (público).
  - `DELETE /api/review/{id}` ⚠️ **pendiente de crear** — sólo para Admin (`[Authorize(Policy = "AdminOnly")]`).
- **Servicio Application:** Agregar método `DeleteAsync(int reviewId)` en `ReviewService` que use `ReviewRepository.Delete()`.
- **Cliente WinForms:** ⚠️ Extender `ReviewApiClient` con `DeleteAsync(int id)`.
- **Forms:** ⚠️ Crear `ReviewModerationForm` en `WinFormsApp/` (selector de propiedad + grilla de reseñas + botón Eliminar).
- **Estado backend:** ⚠️ Requiere agregar un endpoint `DELETE` y su servicio asociado.
