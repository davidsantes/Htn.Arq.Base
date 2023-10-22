using FluentValidation;
using Hacienda.Application.Resources;

namespace Hacienda.Application.Dtos.Categorias
{
    public class InsertCategoriaProductoRequestValidator : AbstractValidator<InsertCategoriaProductoRequest>
    {
        public InsertCategoriaProductoRequestValidator()
        {
            int maxLength = 50;

            RuleFor(categoriaRequest => categoriaRequest.Id)
                .NotNull();

            RuleFor(categoriaRequest => categoriaRequest.Id.Valor.ToString())
                .NotEmpty().WithMessage(ValidationResources.CategoriaIdRequerido)
                .Must(BeConvertibleToInt)
                .WithMessage(ValidationResources.CampoFormatoNoValido);

            RuleFor(categoria => categoria.Nombre)
                .NotEmpty().WithMessage(ValidationResources.CategoriaNombreRequerido)
                .MaximumLength(maxLength)
                .WithMessage(string.Format(ValidationResources.CategoriaNombreMaxLength, maxLength));
        }

        private bool BeConvertibleToInt(string id)
        {
            return int.TryParse(id, out _);
        }
    }
}