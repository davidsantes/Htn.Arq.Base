namespace Hacienda.Domain.Results;

/// <summary>
/// Para poder devolver resultados controlados y tipados.
/// Además de si la operación ha sido exitosa o no, devuelve un genérico que puede representar:
/// el usuario creado, un identificador, etc.
/// </summary>
/// <typeparam name="T">Elemento a devolver</typeparam>
public class ResultToReturnWithObject<T>
{
    public bool IsSuccess { get; }
    public T Value { get; }
    public IDictionary<string, object> Errors { get; }

    private ResultToReturnWithObject(T value, bool isSuccess, Dictionary<string, object> errors)
    {
        if (isSuccess && errors.Count > 0 ||
            !isSuccess && errors.Count < 1)
        {
            throw new ArgumentException("Invalid result", nameof(errors));
        }

        Value = value;
        IsSuccess = isSuccess;
        Errors = errors;
    }

    /// <summary>
    /// Añade un resultado correcto
    /// </summary>
    /// <param name="objeto">Objeto del resultado. Una categoría, un usuario, un id...</param>
    /// <returns>Resultado correcto</returns>
    public static ResultToReturnWithObject<T> AddSuccessResult(T objeto)
    {
        var errorsNone = new Dictionary<string, object>();
        return new ResultToReturnWithObject<T>(objeto, true, errorsNone);
    }

    /// <summary>
    /// Añade un resultado incorrecto
    /// </summary>
    /// <param name="objeto">Objeto del resultado. Una categoría, un usuario, un id...</param>
    /// <param name="code">Código identificativo del error. Se recomienda utilizar una nomenclatura: Usuario.NoCompletado, etc</param>
    /// <param name="description">Descripción de error</param>
    /// <returns>Resultado incorrecto</returns>
    public static ResultToReturnWithObject<T> AddFailureResult(T objeto, string code, string description)
    {
        var errors = new Dictionary<string, object>
        {
            { code, description }
        };
        return new ResultToReturnWithObject<T>(objeto, false, errors);
    }

    /// <summary>
    /// Añade un resultado incorrecto
    /// </summary>
    /// <param name="objeto">Objeto del resultado. Una categoría, un usuario, un id...</param>
    /// <param name="errors">Lista de errores</param>
    /// <returns>Resultado incorrecto</returns>
    public static ResultToReturnWithObject<T> AddFailureResult(T objeto, Dictionary<string, object> errors)
    {
        return new ResultToReturnWithObject<T>(objeto, false, errors);
    }
}