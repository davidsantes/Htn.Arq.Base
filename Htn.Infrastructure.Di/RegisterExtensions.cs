using Htn.Arq.Base.Bll.Services;
using Htn.Arq.Base.Bll.Services.Interfaces;
using Htn.Arq.Base.Dal.Adapters;
using Htn.Arq.Base.Dal.Adapters.Interfaces;
using Htn.Arq.Base.Dal.Repositories;
using Htn.Arq.Base.Dal.Repositories.Interfaces;
using Htn.Infrastructure.Core.Exceptions.Policies.Imp;
using Htn.Infrastructure.Core.Exceptions.Policies.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace Htn.Infrastructure.Di
{
    public static class RegisterExtensions
    {
        /// <summary>
        /// Registra en el contenedor de DI los repositorios
        /// </summary>
        /// <param name="services">Service collection</param>
        /// <param name="isSingleton">Indica si se tiene que registrar como Singleton, o como Scoped. Transient está descartado</param>
        /// <returns>Colección configurada</returns>
        public static IServiceCollection RegisterDalRepositories(
            this IServiceCollection services, 
            bool isSingleton = false)
        {
            if (isSingleton)
            {
                services.AddSingleton<ICategoriaRepository, CategoriaRepository>();
            }
            else
            {
                services.AddScoped<ICategoriaRepository, CategoriaRepository>();
            }

            return services;
        }

        /// <summary>
        /// Registra en el contenedor de DI los adapters para apis y servicios externos
        /// </summary>
        /// <param name="services">Service collection</param>
        /// <param name="isSingleton">Indica si se tiene que registrar como Singleton, o como Scoped. Transient está descartado</param>
        /// <returns>Colección configurada</returns>
        public static IServiceCollection RegisterDalAdapters(
            this IServiceCollection services,
            bool isSingleton = false)
        {
            if (isSingleton)
            {
                services.AddSingleton<ICorreosAdapter, CorreosAdapter>();
            }
            else
            {
                services.AddScoped<ICorreosAdapter, CorreosAdapter>();
            }

            return services;
        }

        /// <summary>
        /// Registra en el contenedor de DI los servicios de dominio 
        /// </summary>
        /// <param name="services">Service collection</param>
        /// <param name="isSingleton">Indica si se tiene que registrar como Singleton, o como Scoped. Transient está descartado</param>
        /// <returns>Colección configurada</returns>
        public static IServiceCollection RegisterBllServices(
            this IServiceCollection services,
            bool isSingleton = false)
        {
            if (isSingleton)
            {
                services.AddSingleton<ICategoriaProductoService, CategoriaProductoService>();
            }
            else
            {
                services.AddScoped<ICategoriaProductoService, CategoriaProductoService>();
            }
            return services;
        }

        /// <summary>
        /// Registra en el contenedor de DI las excepciones personalizadas y sus políticas de autorización
        /// </summary>
        /// <param name="services">Service collection</param>
         /// <returns>Colección configurada</returns>
        public static IServiceCollection RegisterExceptionPolicies(
            this IServiceCollection services)
        {
            //Debe ser singleton porque se utiliza en un middlewares, el cual por defecto es singleton.
            services.AddSingleton<IExceptionPolicy, SanitizeNotCustomExceptionsPolicy>();

            return services;
        }
    }
}