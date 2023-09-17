namespace Htn.Arq.Base.WebApi.Start
{
    public static class RegisterExtensionsAutomapper
    {
        /// <summary>
        /// Registra en el contenedor de DI los profiles de automapper
        /// </summary>
        public static IServiceCollection RegisterAutomapperProfiles(this IServiceCollection services)
        {
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            return services;
        }
    }
}