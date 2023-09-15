using FluentValidation;

namespace Htn.Arq.Base.WebApi.Dto.Validators
{
    public class CategoriaProductoDtoValidator : AbstractValidator<CategoriaProductoDto>
    {
        public CategoriaProductoDtoValidator()
        {
            RuleFor(categoria => categoria.Nombre)
                .NotEmpty().WithMessage("El nombre de la categoría es obligatorio.")
                .MaximumLength(50).WithMessage("El nombre de la categoría no puede tener más de 50 caracteres.");
        }
    }
}