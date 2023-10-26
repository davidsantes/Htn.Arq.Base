using FluentAssertions;
using Hacienda.Application.Exceptions.Sanitize;
using Hacienda.Domain.Exceptions.Base;
using Xunit;

namespace Hacienda.Application.Test.Exceptions.Sanitize;

[Trait("Exceptions", "Sanitize")]
public class SanitizeAllExceptionsPolicyTests
{
    [Fact]
    public void ApplyPolicy_ReturnsGenericGuidException()
    {
        // Arrange
        var policy = new SanitizeAllExceptionsPolicy();
        var sourceException = new Exception("Source Exception");

        // Act
        var sanitizedException = policy.ApplyPolicy(sourceException);

        // Assert
        sanitizedException.Should().NotBeNull().And.BeOfType<GenericGuidException>();
        sanitizedException.InnerException.Should().BeSameAs(sourceException);
    }
}