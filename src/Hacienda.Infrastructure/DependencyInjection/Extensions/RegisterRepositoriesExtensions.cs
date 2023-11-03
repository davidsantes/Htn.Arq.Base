using Hacienda.Domain.Repositories;
using Hacienda.Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace Hacienda.Application.DependencyInjection.Extensions;

public static class RegisterRepositoriesExtensions
{
    /// <summary>
    /// Registra en el contenedor de DI los repositorios
    /// </summary>
    /// <param name="services">Service collection</param>
    /// <returns>Colección configurada</returns>
    public static IServiceCollection RegisterRepositories(
        this IServiceCollection services)
    {
        services.AddScoped<ICategoriaRepositoryPruebaDapper, CategoriaRepositoryPruebaDapper>();
        services.AddScoped<ICategoriaRepository, CategoriaRepository>();

        return services;
    }

    /// <summary>
    /// Registra en el contenedor de DI los repositorios
    /// Necesario para proyectos que necesiten instanciar como singleton (HostedServices, Blazor, etc)
    /// </summary>
    /// <param name="services">Service collection</param>
    /// <returns>Colección configurada</returns>
    public static IServiceCollection RegisterRepositoriesSingleton(
        this IServiceCollection services)
    {
        services.AddSingleton<ICategoriaRepositoryPruebaDapper, CategoriaRepositoryPruebaDapper>();
        services.AddSingleton<ICategoriaRepository, CategoriaRepository>();

        return services;
    }
}