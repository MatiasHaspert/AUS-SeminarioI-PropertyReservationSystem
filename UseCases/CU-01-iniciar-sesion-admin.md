# CU-01: Iniciar sesión como Administrador

## 1. Identificador y Nombre
**CU-01 — Iniciar sesión como Administrador**

## 2. Actor Principal
- **Administrador** del sistema PropertyReservationSystem.

Actores secundarios:
- **Sistema de autenticación** del backend/servidor (emite el token JWT y resuelve el rol).

## 3. Precondiciones
- La aplicación de escritorio **WinFormsApp** se encuentra instalada y ejecutándose en la máquina del Administrador.
- El servidor está accesible en la URL configurada (`https://localhost:7099` por defecto).
- El Administrador posee una cuenta previamente registrada en el sistema cuyo campo `Role` es igual a `Admin`.
- No existe una sesión activa en el `SessionManager` (de existir, se aplica el CU-02 antes).

## 4. Garantías de Éxito (Postcondiciones)
- El `SessionManager` tiene almacenado un **JWT válido** y sus claims decodificados (UserId, Email, Role).
- La aplicación cierra el `LoginForm` y abre el `DashboardForm`.
- Queda registrado el inicio de sesión en los logs del WebApi (si está habilitado).

## 5. Escenario Principal de Éxito (Flujo Básico)
1. El Administrador inicia la aplicación de escritorio.
2. El sistema muestra el formulario de inicio de sesión (`LoginForm`) con los campos *Email* y *Contraseña*.
3. El Administrador ingresa su email y contraseña, y presiona el botón **Iniciar sesión**.
4. El sistema valida que ambos campos no estén vacíos y que el email tenga formato válido.
5. El sistema envía la petición `POST /api/auth/login` al WebApi con las credenciales.
6. El WebApi verifica las credenciales, genera un token JWT con los claims (`sub`, `email`, `role`) y lo retorna junto con los datos del usuario.
7. El sistema verifica que el claim `role` del token sea igual a `Admin`.
8. El sistema almacena el token en `SessionManager` y decodifica los claims.
9. El sistema cierra el `LoginForm` y abre el `DashboardForm`, dando inicio a la sesión.

## 6. Flujos Alternativos / Extensiones

**4a. Campos incompletos o email mal formado**
- 4a-1. El sistema marca en rojo los campos inválidos y muestra el mensaje *"Ingrese un email válido y una contraseña"*.
- 4a-2. El caso de uso retorna al paso 3 sin enviar la petición al servidor.

**5a. El WebApi no responde (sin conexión / servicio caído)**
- 5a-1. El sistema muestra el mensaje *"No se pudo contactar al servidor. Verifique su conexión."*.
- 5a-2. El caso de uso retorna al paso 3.

**6a. Credenciales incorrectas (HTTP 401)**
- 6a-1. El sistema muestra el mensaje *"Email o contraseña incorrectos"*.
- 6a-2. El caso de uso retorna al paso 3.

**7a. El usuario autenticado no tiene rol `Admin`**
- 7a-1. El sistema descarta el token recibido (no lo guarda en `SessionManager`).
- 7a-2. El sistema muestra el mensaje *"Esta aplicación es de uso exclusivo para administradores"*.
- 7a-3. El caso de uso retorna al paso 3.

---

### Nota de Implementación
- **Endpoints WebApi:** `POST /api/auth/login` (existe en `AuthController`). Opcionalmente `GET /api/auth/me` para refrescar los claims tras el login.
- **Cliente WinForms:** `AuthApiClient.LoginAsync()` ya implementado en `WinFormsClient/AuthApiClient.cs`.
- **Forms:** `WinFormsApp/LoginForm.cs` — ya implementa la validación de rol Admin. Sólo requiere documentación y, eventualmente, ajustes de UX (mensajes de error).
- **Estado backend:** ✅ Funcional. No requiere cambios.
