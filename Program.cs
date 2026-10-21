using BagproWebAPI.Models;
using BagproWebAPI.Service;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using Microsoft.Data.SqlClient;
using ConfigurationManager = BagproWebAPI.Service.ConfigurationManager;

var myAllowSpecificOrigins = "_myAllowSpecificOrigins";
var builder = WebApplication.CreateBuilder(args);

Console.WriteLine("==================================");
Console.WriteLine($"ENTORNO: {builder.Environment.EnvironmentName}");
Console.WriteLine($"BASE DE DATOS: {builder.Configuration.GetConnectionString("DefaultConnection")?.Split(';').FirstOrDefault(x => x.Trim().StartsWith("Database=", StringComparison.OrdinalIgnoreCase))}");
Console.WriteLine("==================================");

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("V1", new OpenApiInfo { Version = "V1", Title = "BagPro WebAPI", Description = "" });
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Name = "Authorization",
        Description = "Bearer Authentication with JWT Token",
        Type = SecuritySchemeType.Http
    });
    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme { Reference = new OpenApiReference { Id = "Bearer", Type = ReferenceType.SecurityScheme } },
            new List<string>()
        }
    });
});
builder.Services.AddScoped<ICacheService, CacheService>();

// ======================================================
// CONFIGURACIÓN Y VALIDACIÓN DE CONEXIÓN SQL SERVER
// ======================================================

/// <summary>
/// Obtiene la cadena de conexión correspondiente al entorno
/// de ejecución configurado en ASP.NET Core.
/// </summary>
var connectionString = builder.Configuration
    .GetConnectionString("DefaultConnection");

// Validar que exista una cadena de conexión.
if (string.IsNullOrWhiteSpace(connectionString))
{
    throw new InvalidOperationException(
        "ERROR: No se ha configurado la cadena de conexión."
    );
}

// Obtener información del servidor y base de datos.
var sqlConnection = new SqlConnectionStringBuilder(connectionString);

// Validar base de datos en desarrollo.
if (builder.Environment.IsDevelopment() &&
    !sqlConnection.InitialCatalog.Equals(
        "plasticaribe2",
        StringComparison.OrdinalIgnoreCase))
{
    throw new InvalidOperationException(
        "Development debe utilizar plasticaribe2."
    );
}

// Validar base de datos en producción y producción local.
if ((builder.Environment.IsProduction() ||
     builder.Environment.IsEnvironment("ProductionLocal")) &&
    !sqlConnection.InitialCatalog.Equals(
        "plasticaribe",
        StringComparison.OrdinalIgnoreCase))
{
    throw new InvalidOperationException(
        "Production y ProductionLocal deben utilizar plasticaribe."
    );
}

// Mostrar información de conexión en consola.
Console.WriteLine("==========================================");
Console.WriteLine($"ENTORNO: {builder.Environment.EnvironmentName}");
Console.WriteLine($"SERVIDOR SQL: {sqlConnection.DataSource}");
Console.WriteLine($"BASE DE DATOS: {sqlConnection.InitialCatalog}");
Console.WriteLine("==========================================");


builder.Services.AddDbContext<plasticaribeContext>(options =>
{ 
    options.UseSqlServer(
        connectionString, 
        sqlServerOptionsAction: SqlOptions => 
        { 
            SqlOptions.EnableRetryOnFailure(); 
        }
     ); 
});

// Registrar DbContext conservando los reintentos.
builder.Services.AddAuthentication(opt =>
{
    opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
#pragma warning disable CS8604 // Posible argumento de referencia nulo
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = ConfigurationManager.AppSetting["JWT:ValidIssuer"],
        ValidAudience = ConfigurationManager.AppSetting["JWT:ValidAudience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(ConfigurationManager.AppSetting["JWT:Secret"]))
    };
#pragma warning restore CS8604 // Posible argumento de referencia nulo
});

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: myAllowSpecificOrigins,

        builder =>
        {
            builder.WithOrigins("http://192.168.0.193:5000", "http://192.168.0.85:4700", "http://192.168.0.193:4700", "http://localhost:4200", "http://192.168.0.190:9090")
            .AllowAnyMethod()
            .AllowAnyHeader();
        });
});

var app = builder.Build();
if (app.Configuration.GetValue<bool>("EnableSwagger"))
{
    app.UseSwagger();

    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint(
            "/swagger/V1/swagger.json",
            "BagPro WebAPI"
        );
    });
}
app.UseHttpsRedirection();

app.UseStaticFiles();

// Aplicar CORS antes de autenticación y autorización.
app.UseRouting();

app.UseCors(myAllowSpecificOrigins);

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();