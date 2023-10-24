using Microsoft.Extensions.DependencyInjection;

namespace Hacienda.Shared.DependencyInjection;

public static class RegisterAutomapperProfilesExtensions
{
    /// <summary>
    /// Registra en el contenedor de DI los profiles de automapper
    /// </summary>
    public static IServiceCollection RegisterAutomapperProfiles(
        this IServiceCollection services)
    {
        services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
        return services;
    }
}