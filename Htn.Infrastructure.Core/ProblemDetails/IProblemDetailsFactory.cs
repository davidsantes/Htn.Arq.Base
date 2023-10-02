using ProblemDetailsAspNetCoreMvc = Microsoft.AspNetCore.Mvc;

namespace Htn.Infrastructure.Core.ProblemDetails
{
    public interface IProblemDetailsFactory
    {
        /// <summary>
        /// Genera un ProblemDetails genérico
        /// </summary>
        /// <returns>ProblemDetails generado</returns>
        ProblemDetailsAspNetCoreMvc.ProblemDetails Create(int statusCode
            , string type
            , string title
            , string detail
            , IDictionary<string, object> extensions = null);
        
        /// <summary>
        /// Genera un ProblemDetails indicando que no ha encontrado un recurso concreto (cliente, usuario, etc).
        /// StatusCodes devuelto = StatusCodes.Status404NotFound
        /// </summary>
        /// <returns>ProblemDetails generado</returns>
        ProblemDetailsAspNetCoreMvc.ProblemDetails CreateRecursoNoEncontrado();

        /// <summary>
        /// Genera un ProblemDetails con la lista de problemas especificados.
        /// StatusCodes devuelto = StatusCodes.Status500InternalServerError
        /// </summary>
        /// <param name="extensions">Lista de problemas</param>
        /// <returns>ProblemDetails generado</returns>
        ProblemDetailsAspNetCoreMvc.ProblemDetails CreateProblemaEnBackEnd(IDictionary<string, object> extensions = null);
    }
}
