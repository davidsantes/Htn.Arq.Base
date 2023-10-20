using FluentAssertions;
using Hacienda.Dal.Adapters;
using Xunit;

namespace Hacienda.Dal.Test.Adapters
{
    [Trait("Adapters", "Correos")]
    public class CorreosAdapterRepositoryTests
    {
        [Fact]
        public async Task Dado_UnCorreo_CuandoInsertoNuevo_EntoncesOk()
        {
            // Arrange
            var correosAdapter = new CorreosAdapter(); // Puedes crear una instancia de tu CorreosAdapter

            // Act
            var result = await correosAdapter.InsAsync();

            // Assert
            result.IsSuccess.Should().BeTrue(); // Verifica que el resultado sea exitoso
            result.Value.Should().BeTrue();     // Verifica que el valor devuelto sea verdadero
        }
    }
}