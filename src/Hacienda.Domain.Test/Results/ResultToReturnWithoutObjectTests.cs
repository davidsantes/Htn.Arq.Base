using FluentAssertions;
using Hacienda.Domain.Results;
using Xunit;

namespace Hacienda.Domain.Test.Results
{
    [Trait("Results", "ResultToReturnWithoutObject")]
    public class ResultToReturnWithoutObjectTests
    {
        [Fact]
        public void Dado_Result_CuandoConstructorCorrecto_EntoncesOk()
        {
            // Act

            var resultado = ResultToReturnWithoutObject.AddSuccessResult();

            // Assert
            resultado.IsSuccess.Should().BeTrue();
            resultado.Errors.Should().BeEmpty();
        }

        [Fact]
        public void Dado_Result_CuandoAgregarMensajeDeError_EntoncesDeberiaAgregarError()
        {
            // Arrange
            (string Key, string Message) result1 = ResultErrorMessageFactory.GetMessage("Categoria.NoEncontrada");

            // Act
            var resultado = ResultToReturnWithoutObject.AddFailureResult(result1.Key, result1.Message);

            // Assert
            resultado.IsSuccess.Should().BeFalse();
            resultado.Errors.Should().HaveCount(1);
            resultado.Errors.Should().ContainKey(result1.Key);
            resultado.Errors[result1.Key].Should().Be(result1.Message);
        }

        [Fact]
        public void Dado_Result_CuandoAgregarMensajesDeError_EntoncesDeberiaAgregarErrores()
        {
            // Arrange
            (string Key, string Message) result1 = ResultErrorMessageFactory.GetMessage("Categoria.NoEncontrada");
            (string Key, string Message) result2 = ResultErrorMessageFactory.GetMessage("Producto.NoEncontrado");
            (string Key, string Message) result3 = ResultErrorMessageFactory.GetMessage("Pedido.NoEncontrado");

            var errors = new Dictionary<string, object>
            {
                { result1.Key, result1.Message },
                { result2.Key, result2.Message },
                { result3.Key, result3.Message },
            };

            // Act
            var resultado = ResultToReturnWithoutObject.AddFailureResult(errors);

            // Assert
            resultado.IsSuccess.Should().BeFalse();
            resultado.Errors.Should().HaveCount(3);
            resultado.Errors.Should().ContainKeys(result1.Key, result2.Key, result3.Key);
            resultado.Errors[result1.Key].Should().Be(result1.Message);
            resultado.Errors[result2.Key].Should().Be(result2.Message);
            resultado.Errors[result3.Key].Should().Be(result3.Message);
        }
    }
}