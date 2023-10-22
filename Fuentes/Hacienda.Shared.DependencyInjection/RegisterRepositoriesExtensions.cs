using Hacienda.Domain.Repositories;
using Hacienda.Infrastructure.Repositories;
using Hacienda.Shared.DependencyInjection.Projects;
using Microsoft.Extensions.DependencyInjection;

namespace Hacienda.Shared.DependencyInjection
{
    public static class RegisterRepositoriesExtensions
    {
        /// <summary>
        /// Registra en el contenedor de DI los repositorios
        /// </summary>
        /// <param name="services">Service collection</param>
        /// <param name="project">Indica el tipo de proyecto. En función del mismo, cambia el registro de las clases</param>
        /// <returns>Colección configurada</returns>
        public static IServiceCollection RegisterRepositories(
            this IServiceCollection services,
            ProjectTypes project)
        {
            switch (project)
            {
                case ProjectTypes.WorkerService: //Un worker service es por defecto singleton
                case ProjectTypes.WebBlazorServer:
                    services.AddSingleton<ICategoriaRepository, CategoriaRepository>();
                    break;

                default:
                    services.AddScoped<ICategoriaRepository, CategoriaRepository>();
                    break;
            }

            return services;
        }
    }
}