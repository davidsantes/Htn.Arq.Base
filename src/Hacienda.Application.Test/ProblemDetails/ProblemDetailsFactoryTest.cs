using FluentAssertions;
using FluentValidation;
using FluentValidation.Results;
using Hacienda.Application.Exceptions;
using Hacienda.Application.ProblemDetails;
using Hacienda.Domain.Exceptions.Base;
using Hacienda.Shared.Global.Resources;
using Microsoft.AspNetCore.Http;
using Xunit;
using ProblemDetailsAspNetCoreMvc = Microsoft.AspNetCore.Mvc;

namespace Hacienda.Application.Test.ProblemDetails;

[Trait("ProblemDetails", "ProblemDetailsFactory")]
public class ProblemDetailsFactoryTest
{
    [Fact]
    public void Dado_ProblemDetailsFactory_CuandoCreoValidacionNoCorrecta_EntoncesOk()
    {
        // Arrange
        var factory = new ProblemDetailsFactory();
        var errors = new List<ValidationFailure>
        {
            new ValidationFailure("Campo1", "Error en el campo 1"),
            new ValidationFailure("Campo2", "Error en el campo 2")
        };
        var validationException = new ValidationException("La validación de algo no ha salido bien", errors);

        var expectedError = new ProblemDetailsAspNetCoreMvc.ProblemDetails
        {
            Status = StatusCodes.Status400BadRequest,
            Type = ExceptionConstantsTypes.ExceptionTypeValidationFailure,
            Title = ValidationResources.MsgValidacionKoTitulo,
            Detail = ValidationResources.MsgValidacionKo,
        };
        expectedError.Extensions["errors"] = errors;

        // Act
        var result = factory.GetInvalidValidation(validationException);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeEquivalentTo(expectedError);
    }

    [Fact]
    public void Dado_ProblemDetailsFactory_CuandoCreoRecursoNoEncontrado_EntoncesOk()
    {
        // Arrange
        var factory = new ProblemDetailsFactory();

        var expectedError = new ProblemDetailsAspNetCoreMvc.ProblemDetails
        {
            Status = StatusCodes.Status404NotFound,
            Type = ExceptionConstantsTypes.ExceptionTypeNotFound,
            Title = GlobalResources.MsgRecursoNoEncontradoTitulo,
            Detail = GlobalResources.MsgRecursoNoEncontrado
        };

        // Act
        var result = factory.GetResourceNotFound();

        // Assert
        result.Should().NotBeNull();
        result.Should().BeEquivalentTo(expectedError);
    }

    [Fact]
    public void Dado_ProblemDetailsFactory_CuandoCreoRecursoNoEncontradoATravesDeExcepcion_EntoncesOk()
    {
        // Arrange
        var factory = new ProblemDetailsFactory();
        var message = "No se ha encontrado el recurso";
        var notFoundException = new NotFoundException(message);

        var expectedError = new ProblemDetailsAspNetCoreMvc.ProblemDetails
        {
            Status = StatusCodes.Status404NotFound,
            Type = ExceptionConstantsTypes.ExceptionTypeNotFound,
            Title = GlobalResources.MsgRecursoNoEncontradoTitulo,
            Detail = message
        };

        // Act
        var result = factory.GetResourceNotFound(notFoundException);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeEquivalentTo(expectedError);
    }

    [Fact]
    public void Dado_ProblemDetailsFactory_CuandoCreoExcepcionBackEnd_EntoncesOk()
    {
        // Arrange
        var factory = new ProblemDetailsFactory();

        var expectedError = new ProblemDetailsAspNetCoreMvc.ProblemDetails
        {
            Status = StatusCodes.Status500InternalServerError,
            Type = ExceptionConstantsTypes.ExceptionTypeControlledInBackend,
            Title = GlobalResources.MsgOperacionKoTitulo,
            Detail = GlobalResources.MsgOperacionKo
        };

        var extensions = new Dictionary<string, object> { { "SomeKey", "SomeValue" } };
        expectedError.Extensions[expectedError.Type] = extensions;

        // Act
        var result = factory.GetBackendProblem(extensions);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeEquivalentTo(expectedError);
    }

    [Fact]
    public void Dado_ProblemDetailsFactory_CuandoCreoExcepcionInesperada_EntoncesOk()
    {
        // Arrange
        var factory = new ProblemDetailsFactory();

        var expectedError = new ProblemDetailsAspNetCoreMvc.ProblemDetails
        {
            Status = StatusCodes.Status500InternalServerError,
            Type = ExceptionConstantsTypes.ExceptionTypeUnexpectedException,
            Title = GlobalResources.MsgExcepcionNoControlada,
            Detail = $"[Exception] - Ha sucedido algo inesperado"
        };

        // Act
        var result = factory.GetUnexpectedProblem("Ha sucedido algo inesperado");

        // Assert
        result.Should().NotBeNull();
        result.Should().BeEquivalentTo(expectedError);
    }
}