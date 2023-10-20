using FluentValidation;
using Hacienda.WebApi.Dtos;
using Hacienda.WebApi.Validators;

namespace Hacienda.WebApi.Builder
{
    public static class RegisterExtensionsDtoValidators
    {
        /// <summary>
        /// Registra en el contenedor de DI los validadores de datos de DTOs
        /// </summary>
        public static IServiceCollection RegisterDtoValidators(this IServiceCollection services)
        {
            services.AddTransient<IValidator<CategoriaProductoDto>, CategoriaProductoDtoValidator>();
            return services;
        }
    }
}