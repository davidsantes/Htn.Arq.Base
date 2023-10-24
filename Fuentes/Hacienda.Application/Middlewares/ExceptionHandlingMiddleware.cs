using FluentValidation;
using Hacienda.Application.Exceptions;
using Hacienda.Application.Exceptions.Sanitize;
using Hacienda.Application.ProblemDetails;
using Hacienda.Domain.Exceptions.Generic;
using Hacienda.Shared.Global.Resources;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using ProblemDetailsAspNetCoreMvc = Microsoft.AspNetCore.Mvc;

namespace Hacienda.Shared.Core.Exceptions.Middlewares;

/// <summary>
/// Manejo de excepciones no controladas.
/// Debido al uso en cada petición, se inyectan las mínimas clases en el constructor
/// y se resuelven en el propio método.
/// </summary>
public class ExceptionHandlingMiddleware
{
    //TODO: simplificar. Revisar vídeo de Clean Architecture With .NET 6 And CQRS - Project Setup
    //https://www.youtube.com/watch?v=tLk4pZZtiDY
    private readonly RequestDelegate _next;

    private readonly IServiceProvider _serviceProvider;

    public ExceptionHandlingMiddleware(RequestDelegate next
        , IServiceProvider serviceProvider)
    {
        _next = next;
        _serviceProvider = serviceProvider;
    }

    public async Task InvokeAsync(HttpContext httpContext)
    {
        try
        {
            await _next(httpContext);
        }
        catch (Exception ex)
        {
            //TODO: probar badrequest
            if (ex is ValidationException ||
                ex is NotFoundException)
            {
                await HandleControlledExceptionAsync(httpContext, ex);
            }
            await HandleUnexpectedExceptionAsync(httpContext, ex);
        }
    }

    /// <summary>
    /// Manejo de excepciones de validación de entrada de datos (fluent validation / data annotations).
    /// </summary>
    /// <returns>Error en formato ProblemDetails</returns>
    private async Task HandleControlledExceptionAsync(HttpContext context, Exception exception)
    {
        var problemDetailsFactory = _serviceProvider.GetRequiredService<IProblemDetailsFactory>();

        switch (exception)
        {
            case ValidationException:
                //var problemDetailsValidation = problemDetailsFactory.CreateRecursoNoEncontrado();
                break;
            case NotFoundException:
                var problemDetails = problemDetailsFactory.CreateRecursoNoEncontrado((NotFoundException)exception);
                context.Response.StatusCode = problemDetails.Status.Value;
                await context.Response.WriteAsJsonAsync(problemDetails);
                break;
            default:
                break;
        }


        //var problemDetails = new ProblemDetailsAspNetCoreMvc.ProblemDetails
        //{
        //    Status = StatusCodes.Status400BadRequest,
        //    Type = ExceptionConstantsTypes.ExceptionTypeValidationFailure,
        //    Title = Global_Resources.MsgValidacionKoTitulo,
        //    Detail = Global_Resources.MsgValidacionKo
        //};

        //if (validationException.Errors is not null)
        //{
        //    problemDetails.Extensions["errors"] = validationException.Errors;
        //}

        //context.Response.StatusCode = StatusCodes.Status400BadRequest;
        //await context.Response.WriteAsJsonAsync(problemDetails);
    }

    /// <summary>
    /// Manejo de excepciones no controladas.
    /// Debido al uso constante por cada petición, se inyectan las mínimas clases en el constructor
    /// y se resuelven en el propio método.
    /// </summary>
    /// <returns>Error en formato ProblemDetails</returns>
    private async Task HandleUnexpectedExceptionAsync(HttpContext context, Exception exUnexpectedException)
    {
        var logger = _serviceProvider.GetRequiredService<ILogger<ExceptionHandlingMiddleware>>();
        var exceptionPolicy = _serviceProvider.GetRequiredService<IExceptionPolicy>();
        var exceptionSaneada = exceptionPolicy.ApplyPolicy(exUnexpectedException);
        logger.LogError("[Exception] LogError. Excepción original: {@originalException}. Excepción saneada: {@exceptionSaneada}"
            , exUnexpectedException, exceptionSaneada);

        context.Response.ContentType = "application/json";
        var response = context.Response;
        response.StatusCode = StatusCodes.Status500InternalServerError;

        var excepcionFormatoProblemDetails = new ProblemDetailsAspNetCoreMvc.ProblemDetails
        {
            Status = StatusCodes.Status500InternalServerError,
            Type = ExceptionConstantsTypes.ExceptionTypeUnexpectedException,
            Title = Global_Resources.MsgExcepcionNoControlada,
            Detail = $"[Exception] - " + exceptionSaneada.Message
        };

        context.Response.StatusCode = StatusCodes.Status500InternalServerError;
        await context.Response.WriteAsJsonAsync(excepcionFormatoProblemDetails);
    }
}