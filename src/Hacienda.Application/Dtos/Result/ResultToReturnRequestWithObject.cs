using Hacienda.Domain.Results;

namespace Hacienda.Application.Dtos.Result;

/// <summary>
/// Para poder devolver resultados controlados y tipados.
/// Además de si la operación ha sido exitosa o no, devuelve un genérico que puede representar:
/// el usuario creado, un identificador, etc.
/// </summary>
/// <typeparam name="T">Elemento a devolver</typeparam>
public class ResultToReturnRequestWithObject<T>
{
    public bool IsSuccess => !Errors.Any();
    public T Value { get; }
    public IDictionary<string, object> Errors { get; }

    public ResultToReturnRequestWithObject(T value)
    {
        Value = value;
        Errors = new Dictionary<string, object>();
    }

    /// <summary>
    /// Creación de un ResultRequest a raíz de un Result
    /// </summary>
    public ResultToReturnRequestWithObject(ResultToReturnWithObject<T> result)
    {
        Value = result.Value;
        Errors = new Dictionary<string, object>(result.Errors);
    }
}