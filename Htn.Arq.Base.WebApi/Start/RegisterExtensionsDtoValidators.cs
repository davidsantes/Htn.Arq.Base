using FluentValidation;
using Htn.Arq.Base.WebApi.Dto;
using Htn.Arq.Base.WebApi.Dto.Validators;

namespace Htn.Arq.Base.WebApi.Start
{
    public static class RegisterExtensionsDtoValidators
    {
        /// <summary>
        /// Registra en el contenedor de DI los validadores de datos de DTOs 
        /// </summary>
        public static void RegisterDtoValidators(this IServiceCollection builder)
        {
            builder.AddTransient<IValidator<CategoriaProductoDto>, CategoriaProductoDtoValidator>();
        }
    }
}
