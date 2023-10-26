using FluentAssertions;
using Hacienda.Application.Exceptions.Sanitize;
using Hacienda.Domain.Exceptions.Base;
using Xunit;

namespace Hacienda.Application.Test.Exceptions.Sanitize;

[Trait("Exceptions", "Sanitize")]
public class SanitizeNotControlledExceptionsPolicyTests
{
    [Fact]
    public void ApplyPolicy_ReturnsSourceExceptionForControlledExceptions()
    {
        // Arrange
        var policy = new SanitizeNotControlledExceptionsPolicy();
        var notFoundException = new NotFoundException("Esta es una prueba");
        var operationException = new OperationException();

        // Act
        var sanitizedExceptionWithNotFoundException = policy.ApplyPolicy(notFoundException);
        var sanitizedExceptionWithOperationException = policy.ApplyPolicy(operationException);

        // Assert
        sanitizedExceptionWithNotFoundException.Should().BeSameAs(notFoundException);
        sanitizedExceptionWithOperationException.Should().BeSameAs(operationException);
    }

    [Fact]
    public void ApplyPolicy_ReturnsGenericGuidExceptionForUncontrolledExceptions()
    {
        // Arrange
        var policy = new SanitizeNotControlledExceptionsPolicy();
        var uncontrolledExceptions = new[] { new Exception("Uncontrolled Exception"), new Exception() };

        foreach (var sourceException in uncontrolledExceptions)
        {
            // Act
            var sanitizedException = policy.ApplyPolicy(sourceException);

            // Assert
            sanitizedException.Should().NotBeNull().And.BeOfType<GenericGuidException>();
            sanitizedException.InnerException.Should().BeSameAs(sourceException);
        }
    }
}