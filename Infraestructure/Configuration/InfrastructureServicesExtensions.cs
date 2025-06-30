using Domain.Interface;
using Infraestructure.Context;
using Infraestructure.Models;
using Infraestructure.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

public static class InfrastructureServicesExtensions
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        // Database Connection
        var connectionString = configuration.GetConnectionString("DefaultConnection");

        // Configurar DbContext y la conexión a MySQL
        services.AddDbContext<TransactivaDbContext>(options =>
            options.UseMySql(
                connectionString,
                ServerVersion.AutoDetect(connectionString)
            )
        );
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<IUserRepository<User>, UserRepository>();
        services.AddScoped<IAuthService, AuthService >();
        services.AddScoped<IPaymentServer, PaymentServer >();
        /*
        // Services Register
        services.AddTransient<IUnitOfWork, UnitOfWork>();
        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<IFileService, FileService>();
        services.AddScoped<IUploadFileToAzureStorageService, UploadFileToAzureStorageService>();
        services.AddScoped<IActivityService, ActivityService>();
        */

        return services;
    }
}