using FluentValidation;
using Hacienda.Domain.Exceptions.Generic;
using ProblemDetailsAspNetCoreMvc = Microsoft.AspNetCore.Mvc;

namespace Hacienda.Application.ProblemDetails;

/// <summary>
/// Factoría para la creación de ProblemDetails personalizados
/// Otra alternativa es la utilización de DefaultProblemDetailsFactory, Microsoft.AspNetCore.Mvc.Infrastructure
/// https://github.com/dotnet/aspnetcore/blob/main/src/Mvc/Mvc.Core/src/Infrastructure/DefaultProblemDetailsFactory.cs
/// </summary>
public interface IProblemDetailsFactory
{
    /// <summary>
    /// Genera un ProblemDetails indicando que no ha encontrado un recurso concreto (cliente, usuario, etc).
    /// StatusCodes devuelto = StatusCodes.Status404NotFound
    /// </summary>
    /// <returns>ProblemDetails generado</returns>
    ProblemDetailsAspNetCoreMvc.ProblemDetails CreateRecursoNoEncontrado();

    /// <summary>
    /// Genera un ProblemDetails indicando que no ha encontrado un recurso concreto (cliente, usuario, etc).
    /// StatusCodes devuelto = StatusCodes.Status404NotFound
    /// </summary>
    /// <param name="ex">Excepción de tipo not found</param>
    /// <returns>ProblemDetails generado</returns>
    ProblemDetailsAspNetCoreMvc.ProblemDetails CreateRecursoNoEncontrado(NotFoundException ex);

    /// <summary>
    /// Genera un ProblemDetails con la lista de problemas especificados.
    /// StatusCodes devuelto = StatusCodes.Status500InternalServerError
    /// </summary>
    /// <param name="extensions">Lista de problemas</param>
    /// <returns>ProblemDetails generado</returns>
    ProblemDetailsAspNetCoreMvc.ProblemDetails CreateProblemaEnBackEnd(IDictionary<string, object> extensions = null);

    /// <summary>
    /// Genera un ProblemDetails a raíz de una validación.
    /// </summary>
    /// <param name="ex">Excepción de tipo validación</param>
    /// <returns>ProblemDetails generado</returns>
    ProblemDetailsAspNetCoreMvc.ProblemDetails CreateValidacionIncorrecta(ValidationException ex);

    /// <summary>
    /// Genera un ProblemDetails a raíz de un problema no controlado.
    /// </summary>
    /// <param name="message">mensaje de la excepción</param>
    /// <returns>ProblemDetails generado</returns>
    ProblemDetailsAspNetCoreMvc.ProblemDetails CreateProblemaInesperado(string message);
}
