using FluentAssertions;
using Hacienda.Application.Exceptions.Sanitize;
using Xunit;

namespace Hacienda.Application.Test.Exceptions.Sanitize;

[Trait("Exceptions", "Sanitize")]
public class SanitizeNothingExceptionsPolicyTests
{
    [Fact]
    public void ApplyPolicy_ReturnsSourceExceptionUnchanged()
    {
        // Arrange
        var policy = new SanitizeNothingExceptionsPolicy();
        var sourceException = new Exception("Source Exception");

        // Act
        var sanitizedException = policy.ApplyPolicy(sourceException);

        // Assert
        sanitizedException.Should().BeSameAs(sourceException);
    }
}