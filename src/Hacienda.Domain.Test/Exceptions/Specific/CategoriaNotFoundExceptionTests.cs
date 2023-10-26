using FluentAssertions;
using Hacienda.Domain.Exceptions.Specific;
using Xunit;

namespace Hacienda.Domain.Test.Exceptions.Specific;

[Trait("Exceptions", "Specific")]
public class CategoriaNotFoundExceptionTests
{
    [Fact]
    public void Constructor_SetsCategoriaIdAndMessage()
    {
        // Arrange
        var categoriaId = 123;

        // Act
        var exception = new CategoriaNotFoundException(categoriaId);

        // Assert
        exception.CategoriaId.Should().Be(categoriaId);
        exception.Message.Should().Be($"Categoría con ID {categoriaId} no encontrada");
    }
}