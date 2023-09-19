using Htn.Arq.Base.Bll.Services;
using Htn.Arq.Base.Bll.Services.Interfaces;
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
        /// <returns>Colección configurada</returns>
        public static IServiceCollection RegisterDalRepositories(this IServiceCollection services)
        {
            //Para que mantenga los valores. Si fuera de BDD debería ser Scoped
            services.AddTransient<ICategoriaRepository, CategoriaRepository>();

            return services;
        }

        /// <summary>
        /// Registra en el contenedor de DI los servicios de dominio 
        /// </summary>
        /// <param name="services">Service collection</param>
        /// <returns>Colección configurada</returns>
        public static IServiceCollection RegisterBllServices(this IServiceCollection services)
        {
            // Domain services
            services.AddScoped<ICategoriaProductoService, CategoriaProductoService>();
            return services;
        }

        /// <summary>
        /// Registra en el contenedor de DI las excepciones personalizadas y sus políticas de autorización
        /// </summary>
        /// <param name="services">Service collection</param>
        /// <returns>Colección configurada</returns>
        public static IServiceCollection RegisterException(this IServiceCollection services)
        {
            services.AddSingleton<IExceptionPolicy, SanitizeNotCustomExceptionsPolicy>();
            //services.AddSingleton<IExceptionManager, ExceptionManager>();
            return services;
        }
    }
}