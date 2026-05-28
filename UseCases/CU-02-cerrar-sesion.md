# CU-02: Cerrar sesión

## 1. Identificador y Nombre
**CU-02 — Cerrar sesión**

## 2. Actor Principal
- **Administrador** con sesión activa en la aplicación de escritorio.

## 3. Precondiciones
- El Administrador completó previamente el caso de uso **CU-01** (Iniciar sesión) con éxito.
- El `SessionManager` posee un JWT válido y la aplicación se encuentra en el `DashboardForm` o en cualquiera de los formularios derivados.

## 4. Garantías de Éxito (Postcondiciones)
- El JWT y los claims son removidos del `SessionManager`.
- La aplicación cierra todos los formularios derivados (gestión de usuarios, propiedades, etc.).
- La aplicación muestra nuevamente el `LoginForm`, listo para un nuevo inicio de sesión.

## 5. Escenario Principal de Éxito (Flujo Básico)
1. El Administrador presiona el botón **Cerrar sesión** ubicado en el `DashboardForm`.
2. El sistema muestra un cuadro de diálogo de confirmación: *"¿Está seguro de que desea cerrar sesión?"*.
3. El Administrador confirma la acción.
4. El sistema invoca `SessionManager.Logout()`, eliminando el token y los claims en memoria.
5. El sistema cierra todos los formularios secundarios abiertos.
6. El sistema cierra el `DashboardForm` y muestra el `LoginForm`.

## 6. Flujos Alternativos / Extensiones

**3a. El Administrador cancela la confirmación**
- 3a-1. El sistema cierra el cuadro de diálogo y no realiza ningún cambio.
- 3a-2. El caso de uso termina sin efectos.

**1a. Cierre de la aplicación con la X de la ventana**
- 1a-1. El sistema interpreta el cierre como un logout implícito.
- 1a-2. El sistema ejecuta `SessionManager.Logout()` antes de terminar el proceso para evitar dejar el token en memoria.

---

### Nota de Implementación
- **Cliente WinForms:** `SessionManager.Logout()` ya implementado en `WinFormsClient/SessionManager.cs`.
- **Forms:** `WinFormsApp/DashboardForm.cs` debe incorporar el botón **Cerrar sesión** y manejar el redireccionamiento al `LoginForm`.
- **Backend:** El logout es **client-side only** (no hay endpoint de revocación de tokens). El JWT permanece válido hasta su expiración natural, por lo que no se envía ninguna petición al WebApi.
- **Estado backend:** ✅ No requiere cambios.
