using Infrastructure.Context;
using Domain.Entities;
using Domain.Enums;
using Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using BCrypt.Net;

namespace Infrastructure.DemoData
{
    public class DemoDataSeeder
    {
        public static async Task SeedAsync(AppDbContext context)
        {
            // Limpiar imágenes anteriores
            ResetImageStorage();
            // Eliminar datos anteriores
            await context.Database.ExecuteSqlRawAsync("DELETE FROM Users WHERE Name = 'Owner1'");
            await context.Database.ExecuteSqlRawAsync("DELETE FROM Users WHERE Name = 'Owner2'");
            await context.Database.ExecuteSqlRawAsync("DELETE FROM Users WHERE Name = 'Admin'");
            await context.Database.ExecuteSqlRawAsync("DELETE FROM Users WHERE Email = 'admin1@example.com'");
            await context.Database.ExecuteSqlRawAsync("DELETE FROM Users WHERE Name = 'User1'");
            await context.Database.ExecuteSqlRawAsync("DELETE FROM Users WHERE Name = 'User2'");
            await context.Database.ExecuteSqlRawAsync("DELETE FROM Properties WHERE Title = 'Casa Alpina'");
            await context.Database.ExecuteSqlRawAsync("DELETE FROM Properties WHERE Title = 'Departamento Centro'");
            await context.Database.ExecuteSqlRawAsync("DELETE FROM Properties WHERE Title = 'Chalet Playa'");
            await context.Database.ExecuteSqlRawAsync("DELETE FROM Properties WHERE Title = 'Finca'");
            await context.Database.ExecuteSqlRawAsync("DELETE FROM PropertyImages");

            // Crear usuarios demo
            var owner1 = new User
            {
                Name = "Owner1",
                LastName = "Owner1",
                Email = "owner1@example.com",
                Phone = "1234567890",
                Address = new Address(
                    country: "Argentina",
                    state: "Buenos Aires",
                    city: "Ciudad",
                    postalCode: 1000,
                    streetAddress: "Calle Falsa 123"
                ),
                Role = Role.Owner,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword("owner1")
            };
            context.Users.Add(owner1);

            await context.SaveChangesAsync();

            var owner2 = new User
            {
                Name = "Owner2",
                LastName = "Owner2",
                Email = "owner2@example.com",
                Phone = "123117890",
                Address = new Address(
                    country: "Argentina",
                    state: "Santa fe",
                    city: "Rosario",
                    postalCode: 2000,
                    streetAddress: "Calle Falsa 123444"
                ),
                Role = Role.Owner,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword("owner1234")
            };
            context.Users.Add(owner2);
            await context.SaveChangesAsync();

            var admin1 = new User
            {
                Name = "Admin",
                LastName = "Admin",
                Email = "admin@example.com",
                Phone = "1234567899",
                Address = new Address(
                    country: "Argentina",
                    state: "Buenos Aires",
                    city: "Ciudad",
                    postalCode: 1000,
                    streetAddress: "Av. Admin 100"
                ),
                Role = Role.Admin,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword("admin")
            };
            context.Users.Add(admin1);
            await context.SaveChangesAsync();

            var user1 = new User
            {
                Name = "User1",
                LastName = "User1",
                Email = "user1@example.com",
                Phone = "123117890",
                Address = new Address(
                    country: "Argentina",
                    state: "Santa fe",
                    city: "Rosario",
                    postalCode: 2000,
                    streetAddress: "Calle Falsa 123444"
                ),
                Role = Role.User,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword("user1")
            };
            context.Users.Add(user1);
            await context.SaveChangesAsync();

            var user2 = new User
            {
                Name = "User2",
                LastName = "User2",
                Email = "user2@example.com",
                Phone = "123117890",
                Address = new Address(
                    country: "Argentina",
                    state: "Santa fe",
                    city: "Rosario",
                    postalCode: 2000,
                    streetAddress: "Calle Falsa 123444"
                ),
                Role = Role.User,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword("user2")
            };
            context.Users.Add(user2);
            await context.SaveChangesAsync();

            // Crear propiedad demo
            var property1 = new Property
            {
                Title = "Casa Alpina",
                NightlyPrice = 120000,
                MaxGuests = 3,
                Bedrooms = 3,
                Bathrooms = 2,
                Address = new Address(
                    country: "Argentina",
                    state: "Santa Fe",
                    city: "Rosario",
                    postalCode: 2000,
                    streetAddress: "Calle Montaña 123"
                ),
                Description = """
                    Esta encantadora casa alpina combina estilo rústico y confort moderno.
                    Ideal para familias, ofrece un amplio jardín 🌳 y un entorno tranquilo.

                    🏔️ Ubicada en una zona elevada con aire puro y vistas despejadas.
                    🔥 Incluye estufa a leña y amplios ventanales que aportan luz natural.
                    """,
                OwnerId = owner1.Id
            };
            context.Properties.Add(property1);
            await context.SaveChangesAsync();

            var property2 = new Property
            {
                Title = "Departamento Centro",
                NightlyPrice = 80000,
                MaxGuests = 4,
                Bedrooms = 2,
                Bathrooms = 1,
                Address = new Address(
                    country: "Argentina",
                    state: "Córdoba",
                    city: "Córdoba",
                    postalCode: 5000,
                    streetAddress: "Av. Principal 456"
                ),
                Description = """
                    Departamento moderno en pleno centro de Córdoba. Perfecto para estudiantes o parejas.
 
                    🏙️ Cercano a universidades, comercios y transporte público.
                    🛋️ Ambientes luminosos con excelente distribución y balcón al frente.
                    """,
                OwnerId = owner2.Id
            };
 
            var property3 = new Property
            {
                Title = "Chalet Playa",
                NightlyPrice = 200000,
                MaxGuests = 5,
                Bedrooms = 4,
                Bathrooms = 3,
                Address = new Address(
                    country: "Argentina",
                    state: "Buenos Aires",
                    city: "Mar del Plata",
                    postalCode: 7600,
                    streetAddress: "Camino Costero 789"
                ),
                Description = """
                    Chalet frente al mar ideal para vacaciones o vivienda permanente.
 
                    🌊 Vista directa al océano desde el living.
                    🍃 Cuenta con jardín, garaje y parrilla.
                    ✨ Ambientes amplios y luminosos, a pasos de la playa.
                    """,
                OwnerId = owner1.Id
            };
 
            var property4 = new Property
            {
                Title = "Finca",
                NightlyPrice = 150000,
                MaxGuests = 6,
                Bedrooms = 3,
                Bathrooms = 2,
                Address = new Address(
                    country: "Argentina",
                    state: "Mendoza",
                    city: "Mendoza",
                    postalCode: 5500,
                    streetAddress: "Camino de los Vinos 789"
                ),
                Description = """
                    Finca en una exclusiva zona vitivinícola de Mendoza.
 
                    🍇 Entorno natural con viñedos cercanos.
                    🏡 Estilo rústico y elegante, ideal para inversión turística.
                    🔒 Propiedad ya vendida, no visible para usuarios públicos.
                    """,
                OwnerId = owner2.Id
            };
 
            context.Properties.AddRange(property2, property3, property4);
            await context.SaveChangesAsync();

            // ======== AGREGAR IMÁGENES ========

            // Property 1 — Casa Alpina (cabaña/casa de montaña, ambiente rústico y acogedor)
            await CreateImageForProperty(context, property1, "https://images.unsplash.com/photo-1542718610-a1d656d1884c?q=80&w=800&auto=format&fit=crop", true);
            await CreateImageForProperty(context, property1, "https://images.unsplash.com/photo-1518780664697-55e3ad937233?q=80&w=800&auto=format&fit=crop", false);
            await CreateImageForProperty(context, property1, "https://images.unsplash.com/photo-1510798831971-661eb04b3739?q=80&w=800&auto=format&fit=crop", false);
            await CreateImageForProperty(context, property1, "https://images.unsplash.com/photo-1449158743715-0a90ebb6d2d8?q=80&w=800&auto=format&fit=crop", false);

            // Property 2 — Departamento Centro (apartamento moderno urbano)
            await CreateImageForProperty(context, property2, "https://images.unsplash.com/photo-1502672260266-1c1ef2d93688?q=80&w=800&auto=format&fit=crop", true);
            await CreateImageForProperty(context, property2, "https://images.unsplash.com/photo-1560448204-e02f11c3d0e2?q=80&w=800&auto=format&fit=crop", false);
            await CreateImageForProperty(context, property2, "https://images.unsplash.com/photo-1493809842364-78817add7ffb?q=80&w=800&auto=format&fit=crop", false);
            await CreateImageForProperty(context, property2, "https://images.unsplash.com/photo-1555041469-a586c61ea9bc?q=80&w=800&auto=format&fit=crop", false);

            // Property 3 — Chalet Playa (casa frente al mar, vistas al océano)
            await CreateImageForProperty(context, property3, "https://images.unsplash.com/photo-1499793983690-e29da59ef1c2?q=80&w=800&auto=format&fit=crop", true);
            await CreateImageForProperty(context, property3, "https://images.unsplash.com/photo-1571003123894-1f0594d2b5d9?q=80&w=800&auto=format&fit=crop", false);
            await CreateImageForProperty(context, property3, "https://images.unsplash.com/photo-1507525428034-b723cf961d3e?q=80&w=800&auto=format&fit=crop", false);
            await CreateImageForProperty(context, property3, "https://images.unsplash.com/photo-1522771753037-6333d0b2f568?q=80&w=800&auto=format&fit=crop", false);

            // Property 4 — Finca (estancia vitivinícola, Mendoza)
            await CreateImageForProperty(context, property4, "https://images.unsplash.com/photo-1560493676-04071c5f467b?q=80&w=800&auto=format&fit=crop", true);
            await CreateImageForProperty(context, property4, "https://images.unsplash.com/photo-1510812431401-41d2bd2722f3?q=80&w=800&auto=format&fit=crop", false);
            await CreateImageForProperty(context, property4, "https://images.unsplash.com/photo-1504279577054-b7f4be6f9ee7?q=80&w=800&auto=format&fit=crop", false);
            await CreateImageForProperty(context, property4, "https://images.unsplash.com/photo-1564013799919-ab600027ffc6?q=80&w=800&auto=format&fit=crop", false);

            await context.SaveChangesAsync();

            // Agregar reseñas
            var review1 = new Review
            {
                PropertyId = property1.Id,
                UserId = user1.Id,
                Rating = 5,
                Comment = "Excelente lugar, muy limpio y con una vista increíble.",
                Date = DateTime.UtcNow.AddDays(-10)
            };

            var review2 = new Review
            {
                PropertyId = property1.Id,
                User = admin1,
                Rating = 4,
                Comment = "Hermosa casa, aunque un poco lejos del centro.",
                Date = DateTime.UtcNow.AddDays(-3)
            };

            context.Reviews.AddRange(review1, review2);
            await context.SaveChangesAsync();

            // Agregar disponibilidades
            var availabilities = new List<PropertyAvailability>
            {
                new PropertyAvailability
                {
                    PropertyId = property1.Id,
                    StartDate = new DateOnly(2026, 4, 1),
                    EndDate = new DateOnly(2026, 4, 30)
                },
                new PropertyAvailability
                {
                    PropertyId = property1.Id,
                    StartDate = new DateOnly(2026, 7, 1),
                    EndDate = new DateOnly(2026, 9, 30)
                }
            };
            context.PropertyAvailabilities.AddRange(availabilities);

           
            // Buscamos servicios ya cargados
            var wifi = await context.Amenities.FirstOrDefaultAsync(s => s.Name == "Wi-Fi");
            var gimnasio = await context.Amenities.FirstOrDefaultAsync(s => s.Name == "Gimnasio");

            // Asociamos servicios existentes a la propiedad
            if (wifi != null) property1.Amenities.Add(wifi);
            if (gimnasio != null) property1.Amenities.Add(gimnasio);

            // Guardar todo
            await context.SaveChangesAsync();

            // Agregamos reservas a la propiedad1
            var reservations = new List<Reservation>
            {
                new Reservation
                {
                    PropertyId = property1.Id,
                    GuestId = user1.Id,
                    StartDate = new DateOnly(2026, 4, 1),
                    EndDate = new DateOnly(2026, 4, 3),
                    TotalGuests = 4,
                    TotalPrice = property1.NightlyPrice * 2,
                    Status = ReservationStatus.Completed,
                    CreatedAt = new DateTime(2026, 3, 20)
                },
                new Reservation
                {
                    PropertyId = property1.Id,
                    GuestId = user2.Id,
                    StartDate = new DateOnly(2026, 4, 7),
                    EndDate = new DateOnly(2026, 4, 10),
                    TotalGuests = 2,
                    TotalPrice = property1.NightlyPrice * 3,
                    Status = ReservationStatus.Confirmed,
                    CreatedAt = DateTime.UtcNow.AddDays(-5)
                },
                new Reservation
                {
                    PropertyId = property1.Id,
                    GuestId = user1.Id,
                    StartDate = new DateOnly(2026, 8, 5),
                    EndDate = new DateOnly(2026, 8, 15),
                    TotalGuests = 4,
                    TotalPrice = property1.NightlyPrice * 10,
                    Status = ReservationStatus.Confirmed,
                    CreatedAt = DateTime.UtcNow.AddDays(-5)
                }
            };
            context.Reservations.AddRange(reservations);

            await context.SaveChangesAsync();

            // ======== AGREGAR PAGOS PENDIENTES ========
            var reservation1 = reservations[0];
            var reservation2 = reservations[1];

            // Creamos la carpeta si no existe (solo para entorno local)
            var paymentFolder = Path.Combine(Directory.GetCurrentDirectory(), "Storage", "Payments");
            if (!Directory.Exists(paymentFolder))
            {
                Directory.CreateDirectory(paymentFolder);
            }

            // Archivo fake 1
            var paymentFileId = Guid.NewGuid();
            var paymentFile1 = Path.Combine(paymentFolder, $"{paymentFileId}.png");
            await File.WriteAllTextAsync(paymentFile1, "COMPROBANTE_FAKE_1");

            // Archivo fake 2
            var paymentFileId2 = Guid.NewGuid();
            var paymentFile2 = Path.Combine(paymentFolder, $"{paymentFileId2}.png");
            await File.WriteAllTextAsync(paymentFile2, "COMPROBANTE_FAKE_2");

            var payments = new List<Payment>
            {
                new Payment
                {
                    Id = paymentFileId,
                    ReservationId = reservation1.Id,
                    Method = PaymentMethod.BankTransfer,
                    ProofPath = paymentFile1,
                    Status = PaymentStatus.Approved,
                    Amount = reservation1.TotalPrice,
                    PaymentDate = DateTime.UtcNow.AddDays(-1)
                },
                new Payment
                {
                    Id = paymentFileId2,
                    ReservationId = reservation2.Id,
                    Method = PaymentMethod.PayPal,
                    ProofPath = paymentFile2,
                    Status = PaymentStatus.Approved,
                    Amount = reservation2.TotalPrice,
                    PaymentDate= DateTime.UtcNow
                }
            };

            context.Payment.AddRange(payments);
            await context.SaveChangesAsync();

        }


        private static async Task CreateImageForProperty( AppDbContext context, Property property, string externalUrl, bool isMain)
        {
            // Configuración de rutas
            string baseUrl = "https://localhost:7099";
            string webRootPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot");
            string uploadFolder = Path.Combine(webRootPath, "uploads", "properties", property.Id.ToString());

            // Crear directorio si no existe
            if (!Directory.Exists(uploadFolder))
            {
                Directory.CreateDirectory(uploadFolder);
            }

            // Generar nombre de archivo y rutas
            string fileName = $"{Guid.NewGuid()}.jpg";
            string physicalPath = Path.Combine(uploadFolder, fileName);
            string localUrl = $"{baseUrl}/uploads/properties/{property.Id}/{fileName}";

            try
            {
                using (var httpClient = new HttpClient())
                {
                    // Descargamos la imagen real de internet
                    var imageBytes = await httpClient.GetByteArrayAsync(externalUrl);
                    await File.WriteAllBytesAsync(physicalPath, imageBytes);
                }

                var propertyImage = new PropertyImage
                {
                    PropertyId = property.Id,
                    Url = localUrl, // Guardamos la URL local, no la de Unsplash
                    FileName = fileName,
                    IsMainImage = isMain,
                    CreationDate = DateTime.UtcNow
                };

                context.PropertyImages.Add(propertyImage);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error descargando imagen para propiedad {property.Id}: {ex.Message}");
            }
        }

        private static void ResetImageStorage()
        {
            // Definir la ruta base: wwwroot/uploads
            var webRootPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot");
            var uploadsFolder = Path.Combine(webRootPath, "uploads");

            try
            {
                // Si existe, la borramos con todo su contenido (true = recursivo)
                if (Directory.Exists(uploadsFolder))
                {
                    Directory.Delete(uploadsFolder, recursive: true);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Advertencia al limpiar imágenes: {ex.Message}");
            }
        }
    }
}
