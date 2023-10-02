using Htn.Infrastructure.Core.Exceptions.Entities;
using Htn.Infrastructure.Global.Resources;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Htn.Arq.Base.WebApi.Factories
{
    public class ProblemDetailsFactory: IProblemDetailsFactory
    {
        public ProblemDetails Create(int statusCode
            , string type
            , string title
            , string detail
            , IDictionary<string, object>? extensions = null)
        {
            var problemDetails = new ProblemDetails
            {
                Status = statusCode,
                Type = type,
                Title = title,
                Detail = detail,                
            };

            if (extensions != null)
            {
                problemDetails.Extensions[title] = extensions;
            }

            return problemDetails;
        }

        public ProblemDetails CreateRecursoNoEncontrado()
        {
            return Create(StatusCodes.Status404NotFound
                , ExceptionConstantsTypes.ExceptionTypeNotFound
                , Global_Resources.MsgRecursoNoEncontradoTitulo
                , Global_Resources.MsgRecursoNoEncontrado);
        }

        public ProblemDetails CreateProblemaEnBackEnd(IDictionary<string, object>? extensions = null)
        {
            return Create(StatusCodes.Status500InternalServerError
                , ExceptionConstantsTypes.ExceptionTypeControlledInBackend
                , Global_Resources.MsgOperacionKoTitulo
                , Global_Resources.MsgOperacionKo
                , extensions);
        }
    }
}
