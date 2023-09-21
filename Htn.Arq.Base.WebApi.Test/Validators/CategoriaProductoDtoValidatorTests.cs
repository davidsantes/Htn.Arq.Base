using FluentValidation.TestHelper;
using Htn.Arq.Base.WebApi.Dtos;
using Htn.Arq.Base.WebApi.Resources;
using Htn.Arq.Base.WebApi.Validators;
using System.Resources;
using Xunit;

namespace Htn.Arq.Base.WebApi.Test.Validators
{
    [Trait("Categoria", "ValidacionCategoriaProductoDto")]
    public class CategoriaProductoDtoValidatorTests
    {
        private readonly ResourceManager _resourceManager;

        public CategoriaProductoDtoValidatorTests()
        {
            _resourceManager = new ResourceManager(typeof(ValidationResources));
        }

        [Fact]
        public void Dado_CategoriaProductoDtoValidator_CuandoValoresObligatoriosSiInformados_EntoncesOk()
        {
            // Arrange
            var validator = new CategoriaProductoDtoValidator();
            var categoria = new CategoriaProductoDto
            {
                Id = 1,
                Nombre = "Nombre de categoría válido"
            };

            // Act
            var result = validator.TestValidate(categoria);

            // Assert
            result.ShouldNotHaveAnyValidationErrors();
        }

        [Fact]
        public void Dado_CategoriaProductoDtoValidator_CuandoValoresObligatoriosNoInformados_EntoncesKo()
        {
            // Arrange
            var validator = new CategoriaProductoDtoValidator();
            var categoria = new CategoriaProductoDto
            {
                Id = 1,
                Nombre = "" // Nombre vacío, debería fallar
            };

            var categoriaNombreRequerido = _resourceManager.GetString("CategoriaNombreRequerido");

            // Act
            var result = validator.TestValidate(categoria);

            // Assert
            result.ShouldHaveValidationErrorFor(c => c.Nombre)
                .WithErrorMessage(categoriaNombreRequerido);
        }

        [Fact]
        public void Dado_CategoriaProductoDtoValidator_CuandoValoresObligatoriosMuyLargos_EntoncesKo()
        {
            // Arrange
            var validator = new CategoriaProductoDtoValidator();
            var categoria = new CategoriaProductoDto
            {
                Id = 1,
                Nombre = "Nombre de categoria demasiado extenso para la cantidad de caracteres aceptados"
            };

            int maxLength = 50;
            var categoriaNombreRequerido = string.Format(_resourceManager.GetString("CategoriaNombreMaxLength"), maxLength);

            // Act
            var result = validator.TestValidate(categoria);

            // Assert
            result.ShouldHaveValidationErrorFor(c => c.Nombre)
                .WithErrorMessage(categoriaNombreRequerido);
        }
    }
}