using FluentValidation.TestHelper;
using Hacienda.Application.Dtos;
using Hacienda.Application.Dtos.Categorias.Validators;
using Hacienda.Shared.Global.Resources;
using Xunit;

namespace Hacienda.Application.Test.Dtos.Categorias;

[Trait("Categoria", "ValidacionCategoriaProductoDto")]
public class InsertCategoriaProductoRequestValidatorTests
{
    [Fact]
    public void Dado_CategoriaProductoDtoValidator_CuandoValoresObligatoriosSiEstanInformados_EntoncesOk()
    {
        // Arrange
        var validator = new InsertCategoriaProductoRequestValidator();
        var categoria = new InsertCategoriaProductoRequest(
            nombre: "Nombre de categoría válido",
            descripcion: "Descripción de categoría válido"
            );

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
        var categoria = new InsertCategoriaProductoRequest(
            nombre: "Nombre de categoría válido", descripcion: null
            );

        // Act
        var result = validator.TestValidate(categoria);

        // Assert
        result.ShouldHaveValidationErrorFor(c => c.Descripcion)
            .WithErrorMessage(ValidationResources.CampoObligatorio);
    }

    [Fact]
    public void Dado_CategoriaProductoDtoValidator_CuandoValoresObligatoriosMuyLargos_EntoncesKo()
    {
        // Arrange
        var validator = new InsertCategoriaProductoRequestValidator();
        var categoria = new InsertCategoriaProductoRequest(nombre: "Nombre de categoria demasiado extenso para la cantidad de caracteres aceptados", descripcion: "Descripcion");

        int maxLength = 50;
        var categoriaNombreRequerido = string.Format(ValidationResources.CampoLongitudMaxima, maxLength);

        // Act
        var result = validator.TestValidate(categoria);

        // Assert
        result.ShouldHaveValidationErrorFor(c => c.Nombre)
            .WithErrorMessage(categoriaNombreRequerido);
    }
}