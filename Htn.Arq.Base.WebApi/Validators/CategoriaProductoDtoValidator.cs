using FluentValidation;
using Htn.Arq.Base.WebApi.Dtos;
using Htn.Arq.Base.WebApi.Resources;

namespace Htn.Arq.Base.WebApi.Validators
{
    public class CategoriaProductoDtoValidator : AbstractValidator<CategoriaProductoDto>
    {
        public CategoriaProductoDtoValidator()
        {
            int maxLength = 50;

            RuleFor(categoriaDto => categoriaDto.Id)
                .NotEmpty().WithMessage(ValidationResources.CategoriaIdRequerido)
                .Must(BeConvertibleToInt).WithMessage(ValidationResources.CampoFormatoNoValido);

            RuleFor(categoria => categoria.Nombre)
                .NotEmpty().WithMessage(ValidationResources.CategoriaNombreRequerido)
                .MaximumLength(maxLength).WithMessage(string.Format(ValidationResources.CategoriaNombreMaxLength, maxLength));
        }

        private bool BeConvertibleToInt(string id)
        {
            return int.TryParse(id, out _);
        }
    }
}