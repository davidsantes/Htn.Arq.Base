using FluentAssertions;
using Hacienda.Domain.Exceptions.Base;
using Xunit;

namespace Hacienda.Domain.Test.Exceptions.Base;

[Trait("Exceptions", "Base")]
public class OperationExceptionTests
{
    [Fact]
    public void DefaultConstructor_SetsDefaultMessage()
    {
        // Act
        var exception = new OperationException("An operation error occurred.");

        // Assert
        exception.Message.Should().Be("An operation error occurred.");
    }

    [Fact]
    public void ParameterizedConstructor_SetsCustomMessage()
    {
        // Arrange
        var customMessage = "Custom operation error message";

        // Act
        var exception = new OperationException(customMessage);

        // Assert
        exception.Message.Should().Be(customMessage);
    }
}