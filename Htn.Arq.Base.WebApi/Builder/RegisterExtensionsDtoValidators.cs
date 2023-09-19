using FluentValidation;
using Htn.Arq.Base.WebApi.Dto;
using Htn.Arq.Base.WebApi.Dto.Validators;

namespace Htn.Arq.Base.WebApi.Builder
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
