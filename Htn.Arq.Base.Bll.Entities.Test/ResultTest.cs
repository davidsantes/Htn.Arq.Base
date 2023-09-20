using FluentAssertions;
using Xunit;

namespace Htn.Arq.Base.Bll.Entities.Test
{
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
            resultado.AddErrorMessage("Error 1");

            // Assert
            resultado.Errors.Should().HaveCount(1);
            resultado.Errors.First().Should().Be("Error 1");
        }

        [Fact]
        public void Dado_Result_CuandoAgregarMensajesDeError_EntoncesDeberiaAgregarErrores()
        {
            // Arrange
            var idMoq = 1;
            var resultado = new Result<int>(idMoq);

            // Act
            resultado.AddErrorMessage("Error 1");
            resultado.AddErrorMessage("Error 2");
            resultado.AddErrorMessage("Error 3");

            // Assert
            resultado.Errors.Should().HaveCount(3);
            resultado.Errors.Should().Contain("Error 1", "Error 2", "Error 3");
        }
    }
}