using FluentAssertions;
using Hacienda.Application.Dtos.Result;
using Hacienda.Domain.Results;
using Xunit;

namespace Hacienda.Domain.Test.Results
{
    [Trait("Results", "ResultToReturnRequestWithoutObject")]
    public class ResultToReturnRequestWithoutObjectTests
    {
        [Fact]
        public void Constructor_WithValidValue_ShouldCreateSuccessfulResultToReturnRequestWithObject()
        {
            // Arrange
            var value = 42;

            // Act
            var result = new ResultToReturnRequestWithoutObject();

            // Assert
            result.IsSuccess.Should().BeTrue();
            result.Errors.Should().BeEmpty();
        }

        [Fact]
        public void Constructor_WithResultToReturnWithObject_ShouldCreateResultToReturnRequestWithObject()
        {
            // Arrange
            var resultToReturn = ResultToReturnWithoutObject.AddSuccessResult();

            // Act
            var result = new ResultToReturnRequestWithoutObject(resultToReturn);

            // Assert
            result.Errors.Should().HaveCount(resultToReturn.Errors.Count);
        }

        [Fact]
        public void Constructor_WithResultToReturnWithObject_ShouldNotModifyOriginalResultErrors()
        {
            // Arrange
            (string Key, string Message) result1 = ResultErrorMessageFactory.GetMessage("Categoria.NoEncontrada");
            var resultToReturn = ResultToReturnWithoutObject.AddFailureResult(result1.Key, result1.Message);

            // Act
            var result = new ResultToReturnRequestWithoutObject(resultToReturn);

            // Assert
            result.Errors.Should().HaveCount(1);
            result.Errors.Should().HaveCount(resultToReturn.Errors.Count);
        }
    }
}