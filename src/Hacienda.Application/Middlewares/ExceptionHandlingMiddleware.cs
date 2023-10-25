using FluentValidation;
using Hacienda.Application.Exceptions.Sanitize;
using Hacienda.Application.ProblemDetails;
using Hacienda.Domain.Exceptions.Generic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Hacienda.Shared.Core.Exceptions.Middlewares;

/// <summary>
/// Manejo de excepciones no controladas.
/// Debido al uso en cada petición, se inyectan las mínimas clases en el constructor
/// y se resuelven en el propio método.
/// </summary>
public class ExceptionHandlingMiddleware
{
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
            else
            {
                await HandleUnexpectedExceptionAsync(httpContext, ex);
            }
        }
    }

    /// <summary>
    /// Manejo de excepciones de validación de entrada de datos (fluent validation / data annotations).
    /// </summary>
    /// <returns>Error en formato ProblemDetails</returns>
    private async Task HandleControlledExceptionAsync(HttpContext context, Exception exception)
    {
        var problemDetailsFactory = _serviceProvider.GetRequiredService<IProblemDetailsFactory>();
        if (exception is ValidationException validationException)
        {
            await WriteProblemDetails(context, problemDetailsFactory.GetInvalidValidation(validationException));
        }
        else if (exception is NotFoundException notFoundException)
        {
            await WriteProblemDetails(context, problemDetailsFactory.GetResourceNotFound(notFoundException));
        }
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

        var problemDetailsFactory = _serviceProvider.GetRequiredService<IProblemDetailsFactory>();
        var excepcionFormatoProblemDetails = problemDetailsFactory.GetUnexpectedProblem(exceptionSaneada.Message);

        await WriteProblemDetails(context, excepcionFormatoProblemDetails);
    }

    private async Task WriteProblemDetails(HttpContext context, ProblemDetails problemDetails)
    {
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = problemDetails.Status ?? StatusCodes.Status500InternalServerError;
        await context.Response.WriteAsJsonAsync(problemDetails);
    }
}