using Hacienda.Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Hacienda.Application.DependencyInjection.Extensions;

public static class RegisterServicesExtensions
{
    /// <summary>
    /// Registra en el contenedor de DI los servicios
    /// </summary>
    /// <param name="services">Service collection</param>
    /// <returns>Colección configurada</returns>
    public static IServiceCollection RegisterServices(
        this IServiceCollection services)
    {
        services.AddScoped<ICategoriaProductoService, CategoriaProductoService>();

        return services;
    }

    /// <summary>
    /// Registra en el contenedor de DI los servicios.
    /// Necesario para proyectos que necesiten instanciar como singleton (HostedServices, Blazor, etc)
    /// </summary>
    /// <param name="services">Service collection</param>
    /// <returns>Colección configurada</returns>
    public static IServiceCollection RegisterServicesSingleton(
        this IServiceCollection services)
    {
        services.AddSingleton<ICategoriaProductoService, CategoriaProductoService>();

        return services;
    }
}