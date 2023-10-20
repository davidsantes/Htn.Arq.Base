using FluentValidation;
using Hacienda.Application.Dtos;
using Hacienda.WebApi.Validators;
using Microsoft.Extensions.DependencyInjection;

namespace Hacienda.Shared.DependencyInjection
{
    public static class RegisterRequestValidatorsExtensions
    {
        /// <summary>
        /// Registra en el contenedor de DI los validadores de datos de DTOs
        /// </summary>
        public static IServiceCollection RegisterRequestValidators(
            this IServiceCollection services)
        {
            services.AddTransient<IValidator<InsertCategoriaProductoRequest>, InsertCategoriaProductoRequestValidator>();
            return services;
        }
    }
}