using Htn.Infrastructure.Core.Exceptions.Entities;
using Htn.Infrastructure.Global.Resources;
using Microsoft.AspNetCore.Http;
using ProblemDetailsAspNetCoreMvc = Microsoft.AspNetCore.Mvc;

namespace Htn.Infrastructure.Core.ProblemDetails
{
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
                problemDetails.Extensions[title] = extensions;
            }

            return problemDetails;
        }

        /// <inheritdoc />
        public ProblemDetailsAspNetCoreMvc.ProblemDetails CreateRecursoNoEncontrado()
        {
            return Create(StatusCodes.Status404NotFound
                , ExceptionConstantsTypes.ExceptionTypeNotFound
                , Global_Resources.MsgRecursoNoEncontradoTitulo
                , Global_Resources.MsgRecursoNoEncontrado);
        }

        /// <inheritdoc />
        public ProblemDetailsAspNetCoreMvc.ProblemDetails CreateProblemaEnBackEnd(IDictionary<string, object> extensions = null)
        {
            return Create(StatusCodes.Status500InternalServerError
                , ExceptionConstantsTypes.ExceptionTypeControlledInBackend
                , Global_Resources.MsgOperacionKoTitulo
                , Global_Resources.MsgOperacionKo
                , extensions);
        }
    }
}