using Hacienda.Infrastructure.DbContextDapper;
using Microsoft.Extensions.DependencyInjection;

namespace Hacienda.Shared.DependencyInjection;

//public static class RegisterDapperExtensions
//{
//    /// <summary>
//    /// Registra en el contenedor de DI las dependencias necesarias para usar el NuGet propio de Dapper
//    /// </summary>
//    /// <param name="services">Service collection</param>
//    /// <param name="connection">Conexión a la base de datos</param>
//    /// <returns>Colección configurada</returns>
//    public static IServiceCollection RegisterDapper(
//        this IServiceCollection services, 
//        string connectionString)
//    {
//        var dbConnection = connectionString;
//        services.AddSingleton<IConnectionFactory>(new ConnectionFactory(dbConnection));

//        return services;
//    }
//}