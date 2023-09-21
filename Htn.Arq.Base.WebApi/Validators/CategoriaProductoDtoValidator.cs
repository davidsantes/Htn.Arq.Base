using FluentValidation;
using Htn.Arq.Base.WebApi.Dtos;
using Htn.Arq.Base.WebApi.Resources;
using System.Resources;

namespace Htn.Arq.Base.WebApi.Validators
{
    public class CategoriaProductoDtoValidator : AbstractValidator<CategoriaProductoDto>
    {
        private readonly ResourceManager _resourceManager;

        public CategoriaProductoDtoValidator()
        {
            //TODO: revisar si hacerlo con IStringLocalizer para inyectarlo
            _resourceManager = new ResourceManager(typeof(ValidationResources));

            int maxLength = 50;

            RuleFor(categoria => categoria.Nombre)
                .NotEmpty().WithMessage(_resourceManager.GetString("CategoriaNombreRequerido"))
                .MaximumLength(maxLength).WithMessage(string.Format(_resourceManager.GetString("CategoriaNombreMaxLength"), maxLength));
        }
    }
}