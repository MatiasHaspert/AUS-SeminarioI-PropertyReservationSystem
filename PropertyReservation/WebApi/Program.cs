using Application.Profiles;
using Application.Services;
using Application.Services.Auth;
using Infrastructure.Context;
using Infrastructure.DemoData;
using Infrastructure.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Text.RegularExpressions;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
 
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(connectionString)
    );


// Inject repositories
builder.Services.AddScoped<PropertyRepository>();
builder.Services.AddScoped<ReservationRepository>();
builder.Services.AddScoped<AmenityRepository>();
builder.Services.AddScoped<ReviewRepository>();
builder.Services.AddScoped<PropertyAvailabilityRepository>();
builder.Services.AddScoped<PropertyImageRepository>();
builder.Services.AddScoped<UserRepository>();
builder.Services.AddScoped<PaymentsRepository>();

// Inject services
builder.Services.AddScoped<ReservationService>();
builder.Services.AddScoped<PropertyImageService>();
builder.Services.AddScoped<PropertyAvailabilityService>();
builder.Services.AddScoped<AmenityService>();
builder.Services.AddScoped<ReviewService>();
builder.Services.AddScoped<PropertyService>();
builder.Services.AddScoped<UserService>();
builder.Services.AddScoped<AuthService>();
builder.Services.AddScoped<PasswordService>();
builder.Services.AddScoped<JwtTokenGeneratorService>();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddAutoMapper(typeof(PropertyProfile));
builder.Services.AddScoped<PaymentsService>();
builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<CurrentUserService>();

// Configurar Swagger para JWT Authentication   
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = Microsoft.OpenApi.Models.SecuritySchemeType.Http,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = Microsoft.OpenApi.Models.ParameterLocation.Header,
        Description = "Ingrese el token JWT como: Bearer {token}"
    });

    options.AddSecurityRequirement(new Microsoft.OpenApi.Models.OpenApiSecurityRequirement
    {
        {
            new Microsoft.OpenApi.Models.OpenApiSecurityScheme
            {
                Reference = new Microsoft.OpenApi.Models.OpenApiReference
                {
                    Type = Microsoft.OpenApi.Models.ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
});


builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowReact",
        policy => policy
            .WithOrigins("http://localhost:5173") // URL de tu React
            .AllowAnyMethod()
            .AllowAnyHeader());
});

// 1. Configuraci�n de par�metros de validaci�n 
var jwtSettings = builder.Configuration.GetSection("Jwt");
var key = Encoding.UTF8.GetBytes(jwtSettings["Secret"]!);

var tokenValidationParams = new TokenValidationParameters
{
    ValidateIssuer = true,             // Validar que el token venga de tu servidor
    ValidateAudience = false,          // Validar que el token sea para esta API
    ValidateLifetime = true,           // Validar que no haya expirado
    ValidateIssuerSigningKey = true,   // Validar que la firma sea correcta

    ValidIssuer = jwtSettings["Issuer"],
    // ValidAudience = jwtSettings["Audience"],
    IssuerSigningKey = new SymmetricSecurityKey(key),

    NameClaimType = ClaimTypes.NameIdentifier,
    RoleClaimType = ClaimTypes.Role,
    ClockSkew = TimeSpan.Zero
};

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    // Asignamos los par�metros al middleware est�ndar
    options.TokenValidationParameters = tokenValidationParams;

    options.Events = new JwtBearerEvents
    {
        // Validaci�n manual
        OnMessageReceived = context =>
        {
            var authHeader = context.Request.Headers["Authorization"].FirstOrDefault();
            if (string.IsNullOrEmpty(authHeader)) return Task.CompletedTask;

            // Regex para limpiar el token
            var regex = new Regex(@"[A-Za-z0-9-_]+\.[A-Za-z0-9-_]+\.[A-Za-z0-9-_]+");
            var match = regex.Match(authHeader);

            if (match.Success)
            {
                var tokenLimpio = match.Value;

                try
                {
                    var handler = new JwtSecurityTokenHandler();

                    // Validamos manualmente
                    var principal = handler.ValidateToken(tokenLimpio, tokenValidationParams, out var validatedToken);

                    // Asignamos la identidad al contexto del evento JWT
                    context.Principal = principal;

                    // Forzamos la asignaci�n al contexto HTTP global para que el Controlador lo vea
                    context.HttpContext.User = principal;

                    // Marcamos �xito
                    context.Success();

                    Console.WriteLine($"AUTH MANUAL OK. Usuario: {principal.Identity?.Name}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"ERROR MANUAL SILENCIOSO: {ex.Message}");
                }
            }

            return Task.CompletedTask;
        }
    };
});


// Configurar Authorization, incluyendo pol�ticas basadas en roles "AdminOnly"
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("AdminOnly", policy =>
        policy.RequireRole("Admin"));
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("AllowReact");

app.UseStaticFiles(); // Debe ir antes de MapControllers para servir wwwroot correctamente

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    await DemoDataSeeder.SeedAsync(db);
}

app.Run();
