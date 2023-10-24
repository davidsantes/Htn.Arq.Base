using FluentAssertions;
using Hacienda.Application.Exceptions;
using Hacienda.Application.ProblemDetails;
using Hacienda.Shared.Global.Resources;
using Microsoft.AspNetCore.Http;
using Xunit;
using ProblemDetailsAspNetCoreMvc = Microsoft.AspNetCore.Mvc;

namespace Hacienda.Application.Test.ProblemDetails;

[Trait("ProblemDetails", "ProblemDetailsFactory")]
public class ProblemDetailsFactoryTest
{
    //TODO: incluir test para todos los casos

    //[Fact]
    //public void Dado_ProblemDetailsFactory_CuandoCreoGenerico_EntoncesOk()
    //{         
    //    // Arrange
    //    var factory = new ProblemDetailsFactory();
    //    const int statusCode = 404;
    //    const string type = "some-type";
    //    const string title = "Some Title";
    //    const string detail = "Some Detail";

    //    var expectedError = new ProblemDetailsAspNetCoreMvc.ProblemDetails
    //    {
    //        Status = statusCode,
    //        Type = type,
    //        Title = title,
    //        Detail = detail
    //    };

    //    var extensions = new Dictionary<string, object> { { "SomeKey", "SomeValue" } };
    //    expectedError.Extensions[type] = extensions;

    //    // Act
    //    var result = factory.Create(statusCode, type, title, detail, extensions);

    //    // Assert
    //    result.Should().NotBeNull();
    //    result.Should().BeEquivalentTo(expectedError);
    //}

    //[Fact]
    //public void Dado_ProblemDetailsFactory_CuandoCreoRecursoNoEncontrado_EntoncesOk()
    //{
    //    // Arrange
    //    var factory = new ProblemDetailsFactory();

    //    var expectedError = new ProblemDetailsAspNetCoreMvc.ProblemDetails
    //    {
    //        Status = StatusCodes.Status404NotFound,
    //        Type = ExceptionConstantsTypes.ExceptionTypeNotFound,
    //        Title = Global_Resources.MsgRecursoNoEncontradoTitulo,
    //        Detail = Global_Resources.MsgRecursoNoEncontrado
    //    };

    //    // Act
    //    var result = factory.CreateRecursoNoEncontrado();

    //    // Assert
    //    result.Should().NotBeNull();
    //    result.Should().BeEquivalentTo(expectedError);
    //}

    [Fact]
    public void Dado_ProblemDetailsFactory_CuandoCreoExcepcionBackEnd_EntoncesOk()
    {
        // Arrange
        var factory = new ProblemDetailsFactory();

        var expectedError = new ProblemDetailsAspNetCoreMvc.ProblemDetails
        {
            Status = StatusCodes.Status500InternalServerError,
            Type = ExceptionConstantsTypes.ExceptionTypeControlledInBackend,
            Title = Global_Resources.MsgOperacionKoTitulo,
            Detail = Global_Resources.MsgOperacionKo
        };

        var extensions = new Dictionary<string, object> { { "SomeKey", "SomeValue" } };
        expectedError.Extensions[expectedError.Type] = extensions;

        // Act
        var result = factory.GetBackendProblem(extensions);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeEquivalentTo(expectedError);
    }
}