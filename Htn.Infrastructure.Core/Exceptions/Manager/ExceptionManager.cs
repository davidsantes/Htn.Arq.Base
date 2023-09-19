using Htn.Infrastructure.Core.Exceptions.Policies.Interfaces;
using Microsoft.VisualBasic;
using System.Collections;
using System.Net;
using System.Text.Json;

namespace Htn.Infrastructure.Core.Exceptions.Manager
{
    public class ExceptionManager : IExceptionManager
    {
        //private readonly ILogger<ExceptionManager> _logProvider;
        //private readonly IExceptionPolicy _exceptionPolicy;

        //public ExceptionManager(ILogger<ExceptionManager> logProvider, IExceptionPolicy exceptionPolicy)
        //{
        //    _logProvider = logProvider;
        //    _exceptionPolicy = exceptionPolicy;
        //}

        //public async Task<Exception> HandleException(HttpContext context, Exception ex)
        //{
        //    // Customizamos la excepción
        //    var handledException = _exceptionPolicy.ApplyPolicy(ex);

        //    // Obtenemos el mensaje
        //    var message = GetCustomMessage(context, ex, handledException);

        //    // Grabamos el log
        //    _logProvider.LogError(handledException, message);

        //    // Tratamos la excepción
        //    HandleExceptionData(ex, handledException);

        //    // Obtenemos la respuesta del contexto para asignar el código de estado
        //    context.Response.ContentType = Constants.CONTENT_TYPE_JSON;

        //    var response = context.Response;

        //    // Error que se va a devolver
        //    var errorResponse = new ExceptionResponse { Success = false };

        //    switch (handledException)
        //    {
        //        case ApplicationException exception:
        //            if (exception.Message.Contains(Constants.INVALID_TOKEN))
        //            {
        //                response.StatusCode = (int)HttpStatusCode.Forbidden;
        //                errorResponse.Codigo = HttpStatusCode.Forbidden.ToString();
        //                errorResponse.Message = $"{Constants.WEB_API} - {exception.Message}";
        //                break;
        //            }
        //            response.StatusCode = (int)HttpStatusCode.BadRequest;
        //            errorResponse.Codigo = HttpStatusCode.BadRequest.ToString();
        //            errorResponse.Message = $"{Constants.WEB_API} - {exception.Message}";
        //            break;

        //        case KeyNotFoundException exception:
        //            response.StatusCode = (int)HttpStatusCode.NotFound;
        //            errorResponse.Codigo = HttpStatusCode.NotFound.ToString();
        //            errorResponse.Message = $"{Constants.WEB_API} - {exception.Message}";
        //            break;

        //        default:
        //            response.StatusCode = (int)HttpStatusCode.InternalServerError;
        //            errorResponse.Codigo = HttpStatusCode.InternalServerError.ToString();
        //            errorResponse.Message = $"{Constants.WEB_API} - {handledException.Message}";
        //            break;
        //    }

        //    // Respuesta en el body
        //    var result = JsonSerializer.Serialize(errorResponse);
        //    await context.Response.WriteAsync(result);

        //    return handledException;
        //}

        //private void HandleExceptionData(Exception baseException, Exception handledException)
        //{
        //    if (baseException.Data.Count > 0)
        //    {
        //        foreach (DictionaryEntry dictionaryEntry in baseException.Data)
        //        {
        //            handledException.Data.Add(dictionaryEntry.Key, dictionaryEntry.Value);
        //        }
        //    }
        //}

        //private string GetCustomMessage(HttpContext context, Exception ex, Exception handledException)
        //{
        //    var user = context.User.ToUserInfo();
        //    var guid = GetGuid(handledException);
        //    var message =
        //        $"{Constants.EXCEPTION_TITLE} {guid} {Environment.NewLine}" +
        //        $"SUBJECT: {user.Subject} - USUARIO: {user.FullName} - PRODUCTO: {user.ClientId} - ACCIÓN: {context.Request.Path} {Environment.NewLine}" +
        //        $"MENSAJE: {ex.Message} {Environment.NewLine}" +
        //        $"{handledException.Message}";

        //    return message;
        //}
    }
}