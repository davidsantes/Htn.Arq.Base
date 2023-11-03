using Hacienda.Domain.ExternalClients;
using Hacienda.Infrastructure.ExternalClients;
using Microsoft.Extensions.DependencyInjection;

namespace Hacienda.Application.DependencyInjection.Extensions;

public static class RegisterAdaptersExtensions
{
    /// <summary>
    /// Registra en el contenedor de DI los adapters para apis y servicios externos
    /// </summary>
    /// <param name="services">Service collection</param>
    /// <returns>Colección configurada</returns>
    public static IServiceCollection RegisterAdapters(
        this IServiceCollection services)
    {
        services.AddScoped<ICorreosClientAdapter, CorreosClientAdapter>();

        return services;
    }

    /// <summary>
    /// Registra como singleton en el contenedor de DI los adapters para apis y servicios externos.
    /// Necesario para proyectos que necesiten instanciar como singleton (HostedServices, Blazor, etc)
    /// </summary>
    /// <param name="services">Service collection</param>
    /// <returns>Colección configurada</returns>
    public static IServiceCollection RegisterAdaptersSingleton(
        this IServiceCollection services)
    {
        services.AddSingleton<ICorreosClientAdapter, CorreosClientAdapter>();

        return services;
    }
}