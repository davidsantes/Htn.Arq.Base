﻿using Hacienda.Domain.ExternalClients;
using Hacienda.Infrastructure.ExternalClients;
using Hacienda.Shared.DependencyInjection.Projects;
using Microsoft.Extensions.DependencyInjection;

namespace Hacienda.Shared.DependencyInjection;

public static class RegisterAdaptersExtensions
{
    /// <summary>
    /// Registra en el contenedor de DI los adapters para apis y servicios externos
    /// </summary>
    /// <param name="services">Service collection</param>
    /// <param name="project">Indica el tipo de proyecto. En función del mismo, cambia el registro de las clases</param>
    /// <returns>Colección configurada</returns>
    public static IServiceCollection RegisterAdapters(
        this IServiceCollection services,
        ProjectTypes project)
    {
        switch (project)
        {
            case ProjectTypes.WorkerService: //Un worker service es por defecto singleton
            case ProjectTypes.WebBlazorServer:
                services.AddSingleton<ICorreosClientAdapter, CorreosClientAdapter>();
                break;

            default:
                services.AddScoped<ICorreosClientAdapter, CorreosClientAdapter>();
                break;
        }

        return services;
    }
}