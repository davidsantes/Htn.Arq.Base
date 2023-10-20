using Hacienda.Shared.Core.Exceptions.Middlewares;
using Microsoft.AspNetCore.Builder;

namespace Hacienda.Shared.DependencyInjection
{
    public static class RegisterApplicationExtensions
    {
        /// <summary>
        /// Registra en el contenedor de DI los middlewares de excepciones personalizadas
        /// </summary>
        /// <param name="app">Application Builder</param>
        /// <returns>Application Builder configurada</returns>
        public static IApplicationBuilder UseExceptionHandling(
            this IApplicationBuilder app)
        {
            app.UseMiddleware<ExceptionHandlingMiddleware>();
            return app;
        }
    }
}