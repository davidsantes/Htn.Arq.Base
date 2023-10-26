using FluentAssertions;
using Hacienda.Domain.Exceptions.Base;
using Xunit;

namespace Hacienda.Domain.Test.Exceptions.Base;

[Trait("Exceptions", "Base")]
public class NotFoundExceptionTests
{
    [Fact]
    public void DefaultConstructor_SetsDefaultMessage()
    {
        // Act
        var exception = new NotFoundException("The requested item was not found.");

        // Assert
        exception.Message.Should().Be("The requested item was not found.");
    }

    [Fact]
    public void ParameterizedConstructor_SetsCustomMessage()
    {
        // Arrange
        var customMessage = "Custom not found message";

        // Act
        var exception = new NotFoundException(customMessage);

        // Assert
        exception.Message.Should().Be(customMessage);
    }
}