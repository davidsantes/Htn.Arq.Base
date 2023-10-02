using Htn.Arq.Base.Bll.Services;
using Htn.Arq.Base.Bll.Services.Interfaces;
using Htn.Arq.Base.Dal.Adapters;
using Htn.Arq.Base.Dal.Adapters.Interfaces;
using Htn.Arq.Base.Dal.Repositories;
using Htn.Arq.Base.Dal.Repositories.Interfaces;
using Htn.Infrastructure.Core.Exceptions.Policies.Imp;
using Htn.Infrastructure.Core.Exceptions.Policies.Interfaces;
using Htn.Infrastructure.Core.Layers;
using Htn.Infrastructure.Core.ProblemDetails;
using Microsoft.Extensions.DependencyInjection;

namespace Htn.Infrastructure.Di
{
    public static class RegisterServicesExtensions
    {
        /// <summary>
        /// Registra en el contenedor de DI los repositorios
        /// </summary>
        /// <param name="services">Service collection</param>
        /// <param name="project">Indica el tipo de proyecto. En función del mismo, cambia el registro de las clases</param>
        /// <returns>Colección configurada</returns>
        public static IServiceCollection RegisterDalRepositories(
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

        /// <summary>
        /// Registra en el contenedor de DI los adapters para apis y servicios externos
        /// </summary>
        /// <param name="services">Service collection</param>
        /// <param name="project">Indica el tipo de proyecto. En función del mismo, cambia el registro de las clases</param>
        /// <returns>Colección configurada</returns>
        public static IServiceCollection RegisterDalAdapters(
            this IServiceCollection services,
            ProjectTypes project)
        {
            switch (project)
            {
                case ProjectTypes.WorkerService: //Un worker service es por defecto singleton
                case ProjectTypes.WebBlazorServer:
                    services.AddSingleton<ICorreosAdapter, CorreosAdapter>();
                    break;

                default:
                    services.AddScoped<ICorreosAdapter, CorreosAdapter>();
                    break;
            }

            return services;
        }

        /// <summary>
        /// Registra en el contenedor de DI los servicios de dominio
        /// </summary>
        /// <param name="services">Service collection</param>
        /// <param name="project">Indica el tipo de proyecto. En función del mismo, cambia el registro de las clases</param>
        /// <returns>Colección configurada</returns>
        public static IServiceCollection RegisterBllServices(
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

        /// <summary>
        /// Registra en el contenedor de DI las políticas de saneamiento de excepciones y los problem details
        /// </summary>
        /// <param name="services">Service collection</param>
        /// <returns>Colección configurada</returns>
        public static IServiceCollection RegisterExceptionAndProblemDetails(
            this IServiceCollection services)
        {
            //Debe ser singleton porque se utiliza en un middlewares, el cual por defecto es singleton.
            services.AddSingleton<IExceptionPolicy, SanitizeNotCustomExceptionsPolicy>();
            services.AddSingleton<IProblemDetailsFactory, ProblemDetailsFactory>();

            return services;
        }
    }
}