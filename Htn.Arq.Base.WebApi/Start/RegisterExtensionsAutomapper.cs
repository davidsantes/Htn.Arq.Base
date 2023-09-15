namespace Htn.Arq.Base.WebApi.Start
{
    public static class RegisterExtensionsAutomapper
    {
        /// <summary>
        /// Registra en el contenedor de DI los profiles de automapper
        /// </summary>
        public static void RegisterAutomapperProfiles(this IServiceCollection builder)
        {
            builder.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
        }
    }
}