using System.Reflection;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace Application.Configuration;

public static class ApplicationServicesExtensions
{
    public static IServiceCollection AddApplicationServicesextencions(this IServiceCollection services)
    {
        services.AddMediatR(cfg =>
            cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
        
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
        return services;
    }

}