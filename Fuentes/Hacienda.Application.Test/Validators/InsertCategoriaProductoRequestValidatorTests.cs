using FluentValidation.TestHelper;
using Hacienda.Application.Dtos;
using Hacienda.Application.Resources;
using Hacienda.WebApi.Validators;
using Xunit;

namespace Hacienda.Application.Test.Validators
{
    [Trait("Categoria", "ValidacionCategoriaProductoDto")]
    public class InsertCategoriaProductoRequestValidatorTests
    {
        [Fact]
        public void Dado_CategoriaProductoDtoValidator_CuandoValoresObligatoriosSiEstanInformados_EntoncesOk()
        {
            // Arrange
            var validator = new InsertCategoriaProductoRequestValidator();
            var categoria = new InsertCategoriaProductoRequest
            {
                Id = "1",
                Nombre = "Nombre de categoría válido"
            };

            // Act
            var result = validator.TestValidate(categoria);

            // Assert
            result.ShouldNotHaveAnyValidationErrors();
        }

        [Fact]
        public void Dado_CategoriaProductoDtoValidator_CuandoValoresObligatoriosNoEstanInformados_EntoncesKo()
        {
            // Arrange
            var validator = new InsertCategoriaProductoRequestValidator();
            var categoria = new InsertCategoriaProductoRequest
            {
                Nombre = ""
            };

            // Act
            var result = validator.TestValidate(categoria);

            // Assert
            result.ShouldHaveValidationErrorFor(c => c.Id)
                .WithErrorMessage(ValidationResources.CategoriaIdRequerido);

            result.ShouldHaveValidationErrorFor(c => c.Nombre)
                .WithErrorMessage(ValidationResources.CategoriaNombreRequerido);
        }

        [Fact]
        public void Dado_CategoriaProductoDtoValidator_CuandoValoresObligatoriosMuyLargos_EntoncesKo()
        {
            // Arrange
            var validator = new InsertCategoriaProductoRequestValidator();
            var categoria = new InsertCategoriaProductoRequest
            {
                Id = "1",
                Nombre = "Nombre de categoria demasiado extenso para la cantidad de caracteres aceptados"
            };

            int maxLength = 50;
            var categoriaNombreRequerido = string.Format(ValidationResources.CategoriaNombreMaxLength, maxLength);

            // Act
            var result = validator.TestValidate(categoria);

            // Assert
            result.ShouldHaveValidationErrorFor(c => c.Nombre)
                .WithErrorMessage(categoriaNombreRequerido);
        }
    }
}