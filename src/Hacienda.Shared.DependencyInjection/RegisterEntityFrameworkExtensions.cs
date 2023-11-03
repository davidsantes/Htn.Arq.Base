using Hacienda.Domain.Repositories.Base;
using Hacienda.Infrastructure.DbContextEf;
using Hacienda.Infrastructure.Repositories.Base;
using Hacienda.Shared.DependencyInjection.Options;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Hacienda.Shared.DependencyInjection;

//public static class RegisterEntityFrameworkExtensions
//{
//    /// <summary>
//    /// Registra en el contenedor de DI las utilidades necesarias para configurar Entity Framework
//    /// </summary>
//    /// <param name="services">Service collection</param>
//    /// <param name="project">Indica el tipo de proyecto. En función del mismo, cambia el registro de las clases</param>
//    /// <returns>Colección configurada</returns>
//    public static IServiceCollection RegisterEntityFramework(
//        this IServiceCollection services)
//    {
//        services.AddScoped(typeof(IRepositoryBase<>), typeof(RepositoryBase<>));

//        services.ConfigureOptions<DatabaseOptionsSetup>();

//        services.AddDbContext<EntityDbContext>(
//            (serviceProvider, dbContextOptionBuilder) =>
//        {
//            var databaseOptions = serviceProvider.GetService<IOptions<DatabaseOptions>>()!.Value;

//            dbContextOptionBuilder.UseSqlServer(databaseOptions.ConnectionString, sqlServerAction =>
//            {
//                sqlServerAction.EnableRetryOnFailure(databaseOptions.MaxRetryCount);
//                sqlServerAction.CommandTimeout(databaseOptions.CommandTimeout);
//            });

//            //Sólo para entornos de debug, ya que baja el rendimiento y expone datos sensibles.
//            dbContextOptionBuilder.EnableDetailedErrors(databaseOptions.EnableDetailedErrors);
//            dbContextOptionBuilder.EnableSensitiveDataLogging(databaseOptions.EnableSensitiveDataLogging);

//            //UseQueryTrackingBehavior: optimización de las queries, no realizará seguimiento de las mismas. 
//            dbContextOptionBuilder.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
//        });

//        return services;
//    }
//}