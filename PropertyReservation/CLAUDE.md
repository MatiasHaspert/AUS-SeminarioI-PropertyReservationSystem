# CLAUDE.md

Este archivo proporciona orientación a Claude Code (claude.ai/code) cuando trabaja con el código de este repositorio.

## Descripción general del proyecto

Sistema de reserva de propiedades construido con Arquitectura Limpia (Clean Architecture). La solución (`PropertyReservation.sln`) contiene cinco proyectos: **Domain**, **Infrastructure**, **Application**, **WebApi** (ASP.NET Core 8) y **WebApp** (React + Vite). También existen los proyectos **WinFormsApp** y **WinFormsClient** para un cliente de escritorio.

## Comandos

### Backend (.NET)

```bash
# Compilar toda la solución
dotnet build PropertyReservation.sln

# Ejecutar la API (verificar en consola el puerto HTTPS asignado)
dotnet run --project WebApi/WebApi.csproj

# Agregar una migración de EF Core
dotnet ef migrations add <NombreMigracion> --project Infrastructure --startup-project WebApi

# Aplicar migraciones
dotnet ef database update --project Infrastructure --startup-project WebApi
```

### Frontend (React/Vite)

```bash
cd WebApp
npm install
npm run dev      # http://localhost:5173
npm run build
npm run lint
```

## Arquitectura

**Dependencias entre capas:** Domain ← Infrastructure ← Application ← WebApi

```
WebApp (React)
  ↓  Axios (Bearer JWT)
WebApi Controllers
  ↓
Application Services + AutoMapper DTOs
  ↓
Infrastructure Repositories
  ↓
Domain Entities
  ↓
AppDbContext (EF Core 9, SQL Server LocalDB)
```

**Patrones clave:**
- **Repository Pattern** — cada entidad tiene su repositorio en `Infrastructure/Repositories/`
- **Service Layer** — la lógica de negocio vive en `Application/Services/`; los controladores son delgados
- **State Machine** — las entidades `Reservation` y `Payment` tienen métodos `Accept()`, `Reject()`, `ConfirmPayment()` que garantizan transiciones de estado válidas
- **Soft Delete** — `Property` tiene una bandera `IsDeleted` con un filtro global de EF; usar `.IgnoreQueryFilters()` cuando sea necesario
- **CurrentUserService** — extrae el ID del usuario autenticado desde los claims del JWT; se inyecta en los servicios que necesitan `userId`

## Base de datos

**Cadena de conexión (appsettings.json):**
```
Server=(localdb)\mssqllocaldb;Database=PropertyReservationDB;Trusted_Connection=True
```

Las migraciones están en `Infrastructure/Migrations/`. El `DemoDataSeeder` inicializa las amenidades al arrancar.

**Configuración notable de EF:**
- Conversiones de valor `DateOnly` ↔ `DateTime` para compatibilidad con SQL Server
- `DeleteBehavior.Restrict` en `Review` y `Reservation` → `User` para evitar eliminaciones en cascada
- Índice filtrado único en `PropertyImage`: solo una imagen por propiedad puede ser la imagen principal

## JWT y autenticación

La configuración está en `appsettings.json` bajo `"Jwt"` (Secret, Issuer, Audience, ExpirationHours).  
La política CORS `"AllowReact"` permite únicamente `http://localhost:5173`.

El frontend guarda el token en `localStorage("token")`. `AuthContext` (`WebApp/src/context/`) provee el hook `useAuth()`. Los interceptores de Axios en cada archivo de servicio adjuntan automáticamente el header `Authorization: Bearer`. `ProtectedRoute` envuelve las páginas autenticadas y redirige a `/login` si falta el token.

## Almacenamiento de archivos estáticos

Las imágenes de propiedades se sirven desde `WebApi/wwwroot/uploads/properties/{propertyId}/`.  
Los comprobantes de pago se almacenan en `WebApi/Storage/Payments/`.

## DTOs y Profiles principales

Los profiles de AutoMapper están en `Application/Profiles/`. Los DTOs están agrupados por funcionalidad bajo `Application/DTOs/` (por ejemplo, `DTOs/Payments/`, `DTOs/Property/`, `DTOs/Reservations/`). Al agregar un nuevo mapeo, registrarlo en la clase de profile correspondiente.

## Enums

Definidos en `Domain/Enums/`:
- `Role` — `User`, `Admin`
- `ReservationStatus` — `PendingPayment`, `PaymentUploaded`, `Confirmed`, `Rejected`, `Expired`, `Cancelled`, `Completed`
- `PaymentStatus` — `Pending`, `Approved`, `Rejected`
- `PaymentMethod` — `Transfer`, `Cash`
