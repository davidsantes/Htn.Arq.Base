using FluentAssertions;
using Hacienda.Domain.Entities;
using Xunit;

namespace Hacienda.Domain.Test.Entities;

[Trait("Entities", "Result")]
public class ResultTests
{
    [Fact]
    public void Dado_Result_CuandoConstructorCorrecto_EntoncesOk()
    {
        // Arrange
        var idMoq = 1;

        // Act
        var resultado = new Result<int>(idMoq);

        // Assert
        resultado.IsSuccess.Should().BeTrue();
    }

    [Fact]
    public void Dado_Result_CuandoAgregarMensajeDeError_EntoncesDeberiaAgregarError()
    {
        // Arrange
        var idMoq = 1;
        var resultado = new Result<int>(idMoq);

        // Act
        resultado.AddErrorMessage("Error1", "Error 1");

        // Assert
        resultado.IsSuccess.Should().BeFalse();
        resultado.Errors.Should().ContainKey("Error1");
        resultado.Errors["Error1"].Should().Be("Error 1");
    }

    [Fact]
    public void Dado_Result_CuandoAgregarMensajesDeError_EntoncesDeberiaAgregarErrores()
    {
        // Arrange
        var idMoq = 1;
        var resultado = new Result<int>(idMoq);

        // Act
        resultado.AddErrorMessage("Error1", "Error 1");
        resultado.AddErrorMessage("Error2", "Error 2");
        resultado.AddErrorMessage("Error3", "Error 3");

        // Assert
        resultado.IsSuccess.Should().BeFalse();
        resultado.Errors.Should().HaveCount(3);
        resultado.Errors.Should().ContainKeys("Error1", "Error2", "Error3");
        resultado.Errors["Error1"].Should().Be("Error 1");
        resultado.Errors["Error2"].Should().Be("Error 2");
        resultado.Errors["Error3"].Should().Be("Error 3");
    }
}