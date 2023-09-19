namespace Htn.Arq.Base.WebApi.Middlewares
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