using FluentAssertions;
using Hacienda.Domain.Exceptions.Specific;
using Xunit;

namespace Hacienda.Domain.Test.Exceptions.Specific;

[Trait("Exceptions", "Specific")]
public class CategoriaOperationExceptionTests
{
    [Fact]
    public void ParameterizedConstructor_SetsCategoriaIdAndMessage()
    {
        // Arrange
        var categoriaId = 123;

        // Act
        var exception = new CategoriaOperationException(categoriaId);

        // Assert
        exception.CategoriaId.Should().Be(categoriaId);
        exception.Message.Should().Be($"Categoría con ID {categoriaId} ha realizado una operación no consistente.");
    }

    [Fact]
    public void DefaultConstructor_SetsDefaultMessage()
    {
        // Act
        var exception = new CategoriaOperationException();

        // Assert
        exception.CategoriaId.Should().BeNull();
        exception.Message.Should().Be("Se ha realizado una operación no válida con la categoría.");
    }
}