using FluentValidation;
using Hacienda.Shared.Core.Exceptions.Entities;
using Hacienda.Shared.Core.Exceptions.Policies.Interfaces;
using Hacienda.Shared.Global.Resources;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using ProblemDetailsAspNetCoreMvc = Microsoft.AspNetCore.Mvc;

namespace Hacienda.Shared.Core.Exceptions.Middlewares
{
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
            catch (ValidationException exValidationException)
            {
                await HandleValidationDataExceptionAsync(httpContext, exValidationException);
            }
            catch (Exception exUnexpectedException)
            {
                await HandleExceptionAsync(httpContext, exUnexpectedException);
            }
        }

        /// <summary>
        /// Manejo de excepciones de validación de entrada de datos (fluent validation / data annotations.
        /// </summary>
        /// <returns>Error en formato ProblemDetails</returns>
        private async Task HandleValidationDataExceptionAsync(HttpContext context, ValidationException validationException)
        {
            var problemDetails = new ProblemDetailsAspNetCoreMvc.ProblemDetails
            {
                Status = StatusCodes.Status400BadRequest,
                Type = ExceptionConstantsTypes.ExceptionTypeValidationFailure,
                Title = Global_Resources.MsgValidacionKoTitulo,
                Detail = Global_Resources.MsgValidacionKo
            };

            if (validationException.Errors is not null)
            {
                problemDetails.Extensions["errors"] = validationException.Errors;
            }

            context.Response.StatusCode = StatusCodes.Status400BadRequest;
            await context.Response.WriteAsJsonAsync(problemDetails);
        }

        /// <summary>
        /// Manejo de excepciones no controladas.
        /// Debido al uso constante por cada petición, se inyectan las mínimas clases en el constructor
        /// y se resuelven en el propio método.
        /// </summary>
        /// <returns>Error en formato ProblemDetails</returns>
        private async Task HandleExceptionAsync(HttpContext context, Exception exUnexpectedException)
        {
            var logger = _serviceProvider.GetRequiredService<ILogger<ExceptionHandlingMiddleware>>();
            var exceptionPolicy = _serviceProvider.GetRequiredService<IExceptionPolicy>();
            var exceptionSaneada = exceptionPolicy.ApplyPolicy(exUnexpectedException);
            logger.LogError("[Exception] LogError. Excepción original: {@originalException}. Excepción saneada: {@exceptionSaneada}"
                , exUnexpectedException, exceptionSaneada);

            context.Response.ContentType = ExceptionConstants.ContentTypeJson;
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
}