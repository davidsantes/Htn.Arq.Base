using Hacienda.Application.Services;
using Hacienda.Shared.DependencyInjection.Projects;
using Microsoft.Extensions.DependencyInjection;

namespace Hacienda.Shared.DependencyInjection;

public static class RegisterServicesExtensions
{
    /// <summary>
    /// Registra en el contenedor de DI los servicios
    /// </summary>
    /// <param name="services">Service collection</param>
    /// <param name="project">Indica el tipo de proyecto. En función del mismo, cambia el registro de las clases</param>
    /// <returns>Colección configurada</returns>
    public static IServiceCollection RegisterServices(
        this IServiceCollection services,
        ProjectTypes project)
    {
        switch (project)
        {
            case ProjectTypes.WorkerService: //Un worker service es por defecto singleton
            case ProjectTypes.WebBlazorServer:
                services.AddSingleton<ICategoriaProductoService, CategoriaProductoService>();
                break;

            default:
                services.AddScoped<ICategoriaProductoService, CategoriaProductoService>();
                break;
        }

        return services;
    }
}