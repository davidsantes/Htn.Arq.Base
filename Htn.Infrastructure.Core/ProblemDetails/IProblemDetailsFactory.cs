using Microsoft.AspNetCore.Mvc;

namespace Htn.Arq.Base.WebApi.Factories
{
    public interface IProblemDetailsFactory
    {
        ProblemDetails Create(int statusCode, string type, string title, string detail, IDictionary<string, object>? extensions = null);
        ProblemDetails CreateRecursoNoEncontrado();
        ProblemDetails CreateProblemaEnBackEnd(IDictionary<string, object>? extensions = null);
    }
}
