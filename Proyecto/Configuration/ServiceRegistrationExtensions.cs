using System.Text;
using Application.Configuration;
using Microsoft.AspNetCore.Authentication.JwtBearer;
/*
using Infraestructure.Configuration;
*/

using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
 
namespace Proyecto.Configuration;

public static class ServiceRegistrationExtensions
{
     public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
    {
        // Añadir configuración para Swagger
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen(options =>
        {
            // Definir el esquema de seguridad para Swagger
            options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                In = ParameterLocation.Header,
                Description = "Ingrese el token JWT en el formato: Bearer {token}",
                Name = "Authorization",
                Type = SecuritySchemeType.ApiKey
            });
        
            // Requiere el esquema de seguridad en cada petición
            options.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                    },
                    new string[] {}
                }
            });
        });
        services.AddInfrastructureServices(configuration);
        services.AddApplicationServicesextencions();
        
        // Configuración de autenticación JWT
        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidIssuer = "car-.*dfr",
                    ValidAudience = "acarfa-.@fr4",
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("super-secreta-1234567890-firma-segura!"))
                };
            });

        // Configuración de autorización basada en roles
        services.AddAuthorization(options =>
        {
            options.AddPolicy(name: "Administrador", policy => policy.RequireRole("Admin"));
            options.AddPolicy(name: "Comprador", policy => policy.RequireRole("Comprador"));
            options.AddPolicy(name: "Vendor", policy => policy.RequireRole("Vendor"));
        });

        
        

        return services;
    }
}