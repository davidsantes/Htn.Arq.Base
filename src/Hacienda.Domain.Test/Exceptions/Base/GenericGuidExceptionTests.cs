using FluentAssertions;
using Hacienda.Domain.Exceptions.Base;
using Xunit;

namespace Hacienda.Domain.Test.Exceptions.Base;

[Trait("Exceptions", "Base")]
public class GenericGuidExceptionTests
{
    [Fact]
    public void Constructor_SetsGuidAndInnerException()
    {
        // Arrange
        var innerException = new Exception("Inner Exception");

        // Act
        var exception = new GenericGuidException(innerException);

        // Assert
        exception.Guid.Should().NotBeEmpty();
        exception.InnerException.Should().BeSameAs(innerException);
    }

    [Fact]
    public void Message_ReturnsCorrectErrorMessage()
    {
        // Arrange
        var innerException = new Exception("Inner Exception");
        var exception = new GenericGuidException(innerException);

        // Act
        var message = exception.Message;

        // Assert
        message.Should().Be($"Ha ocurrido un error con el siguiente código de error: {exception.Guid}. Para más detalles consulte con su administrador.");
    }

    [Fact]
    public void ToString_ReturnsFormattedString()
    {
        // Arrange
        var innerException = new Exception("Inner Exception");
        var exception = new GenericGuidException(innerException);

        // Act
        var toString = exception.ToString();

        // Assert
        toString.Should().Contain("Exception GUID: " + exception.Guid);
        toString.Should().Contain("System.Exception: Inner Exception");
    }
}