using Hacienda.Domain.Repositories.Base;
using Hacienda.Infrastructure.DbContextEf;
using Hacienda.Infrastructure.Repositories.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Hacienda.Shared.DependencyInjection;

public static class RegisterEntityFrameworkExtensions
{
    /// <summary>
    /// Registra en el contenedor de DI las utilidades necesarias para configurar Entity Framework
    /// </summary>
    /// <param name="services">Service collection</param>
    /// <param name="project">Indica el tipo de proyecto. En función del mismo, cambia el registro de las clases</param>
    /// <returns>Colección configurada</returns>
    public static IServiceCollection RegisterEntityFramework(
        this IServiceCollection services,
        string connectionString)
    {
        services.AddScoped(typeof(IRepositoryBase<>), typeof(RepositoryBase<>));
        services.AddDbContext<EntityDbContext>(options =>
        {
            options.UseSqlServer(connectionString);
            //UseQueryTrackingBehavior: optimización de las queries, no realizará seguimiento de las mismas. 
            options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
        });

        return services;
    }
}