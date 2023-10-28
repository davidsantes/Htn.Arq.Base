using Hacienda.Domain.Results;

namespace Hacienda.Application.Dtos.Result;

/// <summary>
/// Para poder devolver resultados controlados y tipados. Pensado para mensajes void, que no tengan que devolver usuarios, identificadores, etc.
/// A diferencia de la clase "Result", no contiene ningún elemento de tipo <T>.
/// </summary>
public class ResultToReturnRequestWithoutObject
{
    public bool IsSuccess => !Errors.Any();
    public IDictionary<string, object> Errors { get; }

    public ResultToReturnRequestWithoutObject()
    {
        Errors = new Dictionary<string, object>();
    }

    /// <summary>
    /// Creación de un ResultRequest a raíz de un Result
    /// </summary>
    public ResultToReturnRequestWithoutObject(ResultToReturnWithoutObject result)
    {
        Errors = new Dictionary<string, object>(result.Errors);
    }
}