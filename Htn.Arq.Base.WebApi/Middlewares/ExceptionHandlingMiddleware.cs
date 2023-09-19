using Htn.Infrastructure.Core.Exceptions.Entities;
using Htn.Infrastructure.Core.Exceptions.Policies.Interfaces;
using System.Collections;
using System.Net;
using System.Text.Json;

namespace Htn.Arq.Base.WebApi.Middlewares
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionHandlingMiddleware> _logger;
        private readonly IExceptionPolicy _exceptionPolicy;

        public ExceptionHandlingMiddleware(RequestDelegate next
            , ILogger<ExceptionHandlingMiddleware> logger
            , IExceptionPolicy exceptionPolicy)
        {
            _next = next;
            _logger = logger;
            _exceptionPolicy = exceptionPolicy;
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

        private async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            var handledException = _exceptionPolicy.ApplyPolicy(exception);
            var message = $"{ExceptionConstants.ExceptionTitle} - WEB API: ArquitecturaWebAPI \r\n {exception.Message} \r\n {handledException.Message}";

            _logger.LogError(handledException, message);
            HandleExceptionData(exception, handledException);

            context.Response.ContentType = ExceptionConstants.ContentTypeJson;
            var response = context.Response;

            var errorResponse = new Error();
            switch (handledException)
            {
                case ApplicationException ex:
                    if (ex.Message.Contains(ExceptionConstants.InvalidToken))
                    {
                        response.StatusCode = (int)HttpStatusCode.Forbidden;
                        errorResponse.Codigo = HttpStatusCode.Forbidden.ToString(); errorResponse.Descripcion = $"WEB API: ArquitecturaWebAPI - " + ex.Message;
                        break;
                    }
                    response.StatusCode = (int)HttpStatusCode.BadRequest;
                    errorResponse.Codigo = HttpStatusCode.BadRequest.ToString(); errorResponse.Descripcion = $"WEB API: ArquitecturaWebAPI - " + ex.Message;
                    break;

                case KeyNotFoundException ex:
                    response.StatusCode = (int)HttpStatusCode.NotFound;
                    errorResponse.Codigo = HttpStatusCode.NotFound.ToString(); errorResponse.Descripcion = $"WEB API: ArquitecturaWebAPI - " + ex.Message;
                    break;

                default:
                    response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    errorResponse.Codigo = HttpStatusCode.InternalServerError.ToString(); errorResponse.Descripcion = $"WEB API: ArquitecturaWebAPI - " + handledException.Message;
                    break;
            }
            var result = JsonSerializer.Serialize(errorResponse);
            await context.Response.WriteAsync(result);
        }

        private void HandleExceptionData(Exception baseException, Exception handledException)
        {
            if (baseException.Data.Count > 0)
            {
                foreach (DictionaryEntry dictionaryEntry in baseException.Data)
                {
                    handledException.Data.Add(dictionaryEntry.Key, dictionaryEntry.Value);
                }
            }
        }
    }
}