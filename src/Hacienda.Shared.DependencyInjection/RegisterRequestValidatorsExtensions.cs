using FluentValidation;
using Hacienda.Application.Dtos;
using Hacienda.Application.Dtos.Categorias.Validators;
using Microsoft.Extensions.DependencyInjection;

namespace Hacienda.Shared.DependencyInjection;

//public static class RegisterRequestValidatorsExtensions
//{
//    /// <summary>
//    /// Registra en el contenedor de DI los validadores de datos de DTOs
//    /// </summary>
//    public static IServiceCollection RegisterRequestValidators(
//        this IServiceCollection services)
//    {
//        services.AddTransient<IValidator<InsertCategoriaProductoRequest>, InsertCategoriaProductoRequestValidator>();
//        services.AddTransient<IValidator<UpdateCategoriaProductoRequest>, UpdateCategoriaProductoRequestValidator>();
//        return services;
//    }
//}