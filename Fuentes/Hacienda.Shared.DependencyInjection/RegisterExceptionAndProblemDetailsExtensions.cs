using Hacienda.Shared.Core.Exceptions.Policies.Imp;
using Hacienda.Shared.Core.Exceptions.Policies.Interfaces;
using Hacienda.Shared.Core.ProblemDetails;
using Microsoft.Extensions.DependencyInjection;

namespace Hacienda.Shared.DependencyInjection
{
    public static class RegisterExceptionAndProblemDetailsExtensions
    {
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