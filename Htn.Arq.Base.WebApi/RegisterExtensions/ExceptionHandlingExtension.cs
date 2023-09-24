using Htn.Arq.Base.WebApi.Middlewares;

namespace Htn.Arq.Base.WebApi.RegisterExtensions
{
    public static class ExceptionHandlingExtension
    {
        /// <summary>
        /// Registra en la aplicación el control de excepciones.
        /// Es muy importante que en el flujo se inyecte después de app.UseAuthorization(); o app.MapControllers();
        /// </summary>
        public static IApplicationBuilder UseExceptionHandling(this IApplicationBuilder app)
        {
            app.UseMiddleware<ExceptionHandlingMiddleware>();
            return app;
        }
    }
}
