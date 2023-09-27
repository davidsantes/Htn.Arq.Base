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

            RuleFor(categoria => categoria.Id)
                .NotEmpty().WithMessage(ValidationResources.CategoriaIdRequerido);

            RuleFor(categoria => categoria.Nombre)
                .NotEmpty().WithMessage(ValidationResources.CategoriaNombreRequerido)
                .MaximumLength(maxLength).WithMessage(string.Format(ValidationResources.CategoriaNombreMaxLength, maxLength));
        }
    }
}