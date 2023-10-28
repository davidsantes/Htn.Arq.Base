using FluentAssertions;
using Hacienda.Application.Dtos.Result;
using Hacienda.Domain.Results;
using Xunit;

namespace Hacienda.Application.Test.Dtos.Result
{
    [Trait("Results", "ResultToReturnRequestWithObject")]
    public class ResultToReturnRequestWithObjectTests
    {
        [Fact]
        public void Constructor_WithValidValue_ShouldCreateSuccessfulResultToReturnRequestWithObject()
        {
            // Arrange
            var value = 42;

            // Act
            var result = new ResultToReturnRequestWithObject<int>(value);

            // Assert
            result.IsSuccess.Should().BeTrue();
            result.Value.Should().Be(value);
            result.Errors.Should().BeEmpty();
        }

        [Fact]
        public void Constructor_WithResultToReturnWithObject_ShouldCreateResultToReturnRequestWithObject()
        {
            // Arrange
            var resultToReturn = ResultToReturnWithObject<int>.AddSuccessResult(42);

            // Act
            var result = new ResultToReturnRequestWithObject<int>(resultToReturn.Value);

            // Assert
            result.Value.Should().Be(resultToReturn.Value);
            result.Errors.Should().HaveCount(0);
            result.Errors.Should().HaveCount(resultToReturn.Errors.Count);
        }

        [Fact]
        public void Constructor_WithResultToReturnWithObject_ShouldNotModifyOriginalResultErrors()
        {
            // Arrange
            (string Key, string Message) result1 = ResultErrorMessageFactory.GetMessage("Categoria.NoEncontrada");
            var resultToReturn = ResultToReturnWithObject<int>.AddFailureResult(42, result1.Key, result1.Message);

            // Act
            var result = new ResultToReturnRequestWithObject<int>(resultToReturn);

            // Assert
            result.Errors.Should().HaveCount(1);
            result.Errors.Should().HaveCount(resultToReturn.Errors.Count);
        }
    }
}