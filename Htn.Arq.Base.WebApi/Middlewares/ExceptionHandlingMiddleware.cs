using Htn.Infrastructure.Core.Exceptions.Entities;
using Htn.Infrastructure.Core.Exceptions.Policies.Interfaces;
using Htn.Infrastructure.Global.Resources;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace Htn.Arq.Base.WebApi.Middlewares
{
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
                await HandleExceptionAsync(httpContext, ex);
            }
        }

        /// <summary>
        /// Manejo de la excepción.
        /// Debido al uso constante por cada petición, se inyectan las mínimas clases en el constructor
        /// y se resuelven en el propio método.
        /// </summary>
        /// <returns>Error en formato ProblemDetails</returns>
        private async Task HandleExceptionAsync(HttpContext context, Exception originalException)
        {
            var logger = _serviceProvider.GetRequiredService<ILogger<ExceptionHandlingMiddleware>>();
            var exceptionPolicy = _serviceProvider.GetRequiredService<IExceptionPolicy>();
            var exceptionSaneada = exceptionPolicy.ApplyPolicy(originalException);
            logger.LogError("[Exception] LogError. Excepción original: {@originalException}. Excepción saneada: {@exceptionSaneada}", originalException, exceptionSaneada);

            context.Response.ContentType = ExceptionConstants.ContentTypeJson;
            var response = context.Response;
            response.StatusCode = StatusCodes.Status500InternalServerError;

            var excepcionFormatoProblemDetails = new ProblemDetails
            {
                Title = Global_Resources.MsgExcepcionNoControlada,
                Detail = $"[Exception] - " + exceptionSaneada.Message,
                Status = StatusCodes.Status500InternalServerError
            };

            var result = JsonSerializer.Serialize(excepcionFormatoProblemDetails);
            await context.Response.WriteAsync(result);
        }
    }
}