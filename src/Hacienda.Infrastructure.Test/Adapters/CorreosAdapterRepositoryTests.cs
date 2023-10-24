using FluentAssertions;
using Hacienda.Infrastructure.ExternalClients;
using Xunit;

namespace Hacienda.Infrastructure.Test.Adapters;

[Trait("Adapters", "Correos")]
public class CorreosAdapterRepositoryTests
{
    [Fact]
    public async Task Dado_UnCorreo_CuandoInsertoNuevo_EntoncesOk()
    {
        // Arrange
        var correosAdapter = new CorreosClientAdapter();

        // Act
        var result = await correosAdapter.InsAsync();

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.Value.Should().BeTrue();
    }
}