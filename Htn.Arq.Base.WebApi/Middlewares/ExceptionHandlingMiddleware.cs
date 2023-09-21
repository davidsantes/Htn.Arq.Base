using Htn.Infrastructure.Core.Exceptions.Entities;
using Htn.Infrastructure.Core.Exceptions.Policies.Interfaces;
using System.Net;
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

        private async Task HandleExceptionAsync(HttpContext context, Exception originalException)
        {
            //Debido al uso constante por cada petición, se inyectan las mínimas clases en el constructor.
            //Se resuelven aquí:
            var logger = _serviceProvider.GetRequiredService<ILogger<ExceptionHandlingMiddleware>>();
            var exceptionPolicy = _serviceProvider.GetRequiredService<IExceptionPolicy>();

            var exceptionSaneada = exceptionPolicy.ApplyPolicy(originalException);
            var message = $"{ExceptionConstants.ExceptionTitle} - WEB API: ArquitecturaWebAPI \r\n" +
                          $" Excepción original: {originalException.Message} \r\n" +
                          $" Excepción saneada: {exceptionSaneada.Message}";

            logger.LogError(exceptionSaneada, message);

            context.Response.ContentType = ExceptionConstants.ContentTypeJson;
            var response = context.Response;
            var errorResponse = new Error();

            switch (exceptionSaneada)
            {
                case CustomException:
                    response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    errorResponse.Codigo = HttpStatusCode.InternalServerError.ToString();
                    errorResponse.Descripcion = $"WEB API: ArquitecturaWebAPI - " + exceptionSaneada.Message;
                    break;
                default:
                    response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    errorResponse.Codigo = HttpStatusCode.InternalServerError.ToString();
                    errorResponse.Descripcion = $"WEB API: ArquitecturaWebAPI - " + exceptionSaneada.Message;
                    break;
            }
            var result = JsonSerializer.Serialize(errorResponse);
            await context.Response.WriteAsync(result);
        }
    }
}