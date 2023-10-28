using Hacienda.Domain.Results;

namespace Hacienda.Application.Dtos.Result;

/// <summary>
/// Patrón result pattern, para poder devolver resultados controlados y tipados
/// </summary>
/// <typeparam name="T">Elemento a devolver</typeparam>
public class ResultRequest<T>
{
    public bool IsSuccess => !Errors.Any();
    public T Value { get; }
    public IDictionary<string, object> Errors { get; }

    public ResultRequest(T value)
    {
        Value = value;
        Errors = new Dictionary<string, object>();
    }

    /// <summary>
    /// Creación de un ResultRequest a raíz de un Result
    /// </summary>
    public ResultRequest(Result<T> result)
    {
        Value = result.Value;
        Errors = new Dictionary<string, object>(result.Errors);
    }
}