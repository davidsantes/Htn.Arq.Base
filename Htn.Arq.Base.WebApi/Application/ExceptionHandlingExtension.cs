using Htn.Arq.Base.WebApi.Middlewares;

namespace Htn.Arq.Base.WebApi.Application
{
    public static class ExceptionHandlingExtension
    {
        /// <summary>
        /// Registra en la aplicación el control de excepciones
        /// </summary>
        public static IApplicationBuilder UseExceptionHandling(this IApplicationBuilder app)
        {
            //app.UseMiddleware<ExceptionHandlingMiddleware>();
            app.UseMiddleware<ErrorHandlerMiddleware>();
            return app;
        }
    }
}
