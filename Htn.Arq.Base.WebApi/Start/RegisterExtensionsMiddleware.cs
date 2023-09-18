using Htn.Arq.Base.WebApi.MiddleWares;

namespace Htn.Arq.Base.WebApi.Start
{
    public static class RegisterExtensionsMiddleware
    {
        /// <summary>
        /// Registra en el contenedor de DI los middlewares
        /// </summary>
        public static IServiceCollection RegisterMiddlewares(this IServiceCollection services)
        {
            services.AddTransient<IMiddleware, MyCustomMiddleware>();
            return services;
        }
    }
}