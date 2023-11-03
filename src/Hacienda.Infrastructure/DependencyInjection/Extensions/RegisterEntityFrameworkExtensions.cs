using Hacienda.Domain.Repositories.Base;
using Hacienda.Infrastructure.DbContextEf;
using Hacienda.Infrastructure.DependencyInjection.Settings;
using Hacienda.Infrastructure.Repositories.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Hacienda.Application.DependencyInjection.Extensions;

public static class RegisterEntityFrameworkExtensions
{
    /// <summary>
    /// Registra en el contenedor de DI las utilidades necesarias para configurar Entity Framework
    /// </summary>
    /// <param name="services">Service collection</param>
    /// <param name="project">Indica el tipo de proyecto. En función del mismo, cambia el registro de las clases</param>
    /// <returns>Colección configurada</returns>
    public static IServiceCollection RegisterEntityFramework(
        this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped(typeof(IRepositoryBase<>), typeof(RepositoryBase<>));

        services.Configure<DatabaseEntityFrameworkOptions>(configuration.GetSection(DatabaseEntityFrameworkOptions.SectionName));

        services.AddDbContext<EntityDbContext>(
            (serviceProvider, dbContextOptionBuilder) =>
        {
            var databaseOptions = serviceProvider.GetService<IOptions<DatabaseEntityFrameworkOptions>>()!.Value;

            dbContextOptionBuilder.UseSqlServer(databaseOptions.ConnectionString, sqlServerAction =>
            {
                sqlServerAction.EnableRetryOnFailure(databaseOptions.MaxRetryCount);
                sqlServerAction.CommandTimeout(databaseOptions.CommandTimeout);
            });

            //Sólo para entornos de debug, ya que baja el rendimiento y expone datos sensibles.
            dbContextOptionBuilder.EnableDetailedErrors(databaseOptions.EnableDetailedErrors);
            dbContextOptionBuilder.EnableSensitiveDataLogging(databaseOptions.EnableSensitiveDataLogging);

            //UseQueryTrackingBehavior: optimización de las queries, no realizará seguimiento de las mismas.
            dbContextOptionBuilder.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
        });

        return services;
    }

    /// <summary>
    /// Registra en el contenedor de DI las utilidades necesarias para configurar Entity Framework
    /// </summary>
    /// <param name="services">Service collection</param>
    /// <param name="project">Indica el tipo de proyecto. En función del mismo, cambia el registro de las clases</param>
    /// <returns>Colección configurada</returns>
    public static IServiceCollection RegisterEntityFrameworkTransient(
        this IServiceCollection services, IConfiguration configuration)
    {
        services.AddTransient(typeof(IRepositoryBase<>), typeof(RepositoryBase<>));

        services.Configure<DatabaseEntityFrameworkOptions>(configuration.GetSection(DatabaseEntityFrameworkOptions.SectionName));

        services.AddTransient<EntityDbContext>(
            (serviceProvider) =>
            {
                var databaseOptions = serviceProvider.GetService<IOptions<DatabaseEntityFrameworkOptions>>()!.Value;

                var dbContextOptionBuilder = new DbContextOptionsBuilder();
                dbContextOptionBuilder.UseSqlServer(databaseOptions.ConnectionString, sqlServerAction =>
                {
                    sqlServerAction.EnableRetryOnFailure(databaseOptions.MaxRetryCount);
                    sqlServerAction.CommandTimeout(databaseOptions.CommandTimeout);
                });

                //Sólo para entornos de debug, ya que baja el rendimiento y expone datos sensibles.
                dbContextOptionBuilder.EnableDetailedErrors(databaseOptions.EnableDetailedErrors);
                dbContextOptionBuilder.EnableSensitiveDataLogging(databaseOptions.EnableSensitiveDataLogging);

                //UseQueryTrackingBehavior: optimización de las queries, no realizará seguimiento de las mismas.
                dbContextOptionBuilder.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);

                return new EntityDbContext(dbContextOptionBuilder.Options);
            });

        return services;
    }
}