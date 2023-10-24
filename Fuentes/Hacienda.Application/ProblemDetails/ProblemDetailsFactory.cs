using Hacienda.Application.Exceptions;
using Hacienda.Domain.Exceptions.Generic;
using Hacienda.Shared.Global.Resources;
using Microsoft.AspNetCore.Http;
using ProblemDetailsAspNetCoreMvc = Microsoft.AspNetCore.Mvc;

namespace Hacienda.Application.ProblemDetails;

/// <inheritdoc />
public class ProblemDetailsFactory : IProblemDetailsFactory
{
    /// <inheritdoc />
    public ProblemDetailsAspNetCoreMvc.ProblemDetails Create(int statusCode
        , string type
        , string title
        , string detail
        , IDictionary<string, object> extensions = null)
    {
        var problemDetails = new ProblemDetailsAspNetCoreMvc.ProblemDetails
        {
            Status = statusCode,
            Type = type,
            Title = title,
            Detail = detail,
        };

        if (extensions != null)
        {
            problemDetails.Extensions[type] = extensions;
        }

        return problemDetails;
    }

    public ProblemDetailsAspNetCoreMvc.ProblemDetails CreateRecursoNoEncontrado(NotFoundException ex)
    {
        var problemDetails = Create(statusCode: StatusCodes.Status404NotFound
            , type: ExceptionConstantsTypes.ExceptionTypeNotFound
            , title: string.Format(Global_Resources.MsgRecursoNoEncontradoTitulo)
            , detail: ex.Message);
        return problemDetails;
    }

    /// <inheritdoc />
    public ProblemDetailsAspNetCoreMvc.ProblemDetails CreateRecursoNoEncontrado()
    {
        var problemDetails = Create(statusCode: StatusCodes.Status404NotFound
            , type: ExceptionConstantsTypes.ExceptionTypeNotFound
            , title: Global_Resources.MsgRecursoNoEncontradoTitulo
            , detail: Global_Resources.MsgRecursoNoEncontrado);
        return problemDetails;
    }

    /// <inheritdoc />
    public ProblemDetailsAspNetCoreMvc.ProblemDetails CreateProblemaEnBackEnd(IDictionary<string, object> extensions = null)
    {
        var problemDetails = Create(statusCode: StatusCodes.Status500InternalServerError
            , type: ExceptionConstantsTypes.ExceptionTypeControlledInBackend
            , title: Global_Resources.MsgOperacionKoTitulo
            , detail: Global_Resources.MsgOperacionKo
            , extensions: extensions);
        return problemDetails;
    }
}