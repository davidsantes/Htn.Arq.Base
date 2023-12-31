﻿namespace Hacienda.Domain.Results;

/// <summary>
/// Para poder devolver resultados controlados y tipados. Pensado para mensajes void, que no tengan que devolver usuarios, identificadores, etc.
/// A diferencia de la clase "Result", no contiene ningún elemento de tipo <T>.
/// </summary>
public class ResultToReturnWithoutObject
{
    public bool IsSuccess { get; }
    public IDictionary<string, object> Errors { get; }

    private ResultToReturnWithoutObject(bool isSuccess, Dictionary<string, object> errors)
    {
        if (isSuccess && errors.Count > 0 ||
            !isSuccess && errors.Count < 1)
        {
            throw new ArgumentException("Invalid result", nameof(errors));
        }

        IsSuccess = isSuccess;
        Errors = errors;
    }

    /// <summary>
    /// Añade un resultado correcto
    /// </summary>
    /// <returns>Resultado correcto</returns>
    public static ResultToReturnWithoutObject AddSuccessResult()
    {
        var errorsNone = new Dictionary<string, object>();
        return new ResultToReturnWithoutObject(true, errorsNone);
    }

    /// <summary>
    /// Añade un resultado incorrecto
    /// </summary>
    /// <param name="code">Código identificativo del error. Se recomienda utilizar una nomenclatura: Usuario.NoCompletado, etc</param>
    /// <param name="description">Descripción de error</param>
    /// <returns>Resultado incorrecto</returns>
    public static ResultToReturnWithoutObject AddFailureResult(string code, string description)
    {
        var errors = new Dictionary<string, object>
        {
            { code, description }
        };
        return new ResultToReturnWithoutObject(false, errors);
    }

    /// <summary>
    /// Añade un resultado incorrecto
    /// </summary>
    /// <param name="errors">Lista de errores</param>
    /// <returns>Resultado incorrecto</returns>
    public static ResultToReturnWithoutObject AddFailureResult(Dictionary<string, object> errors)
    {
        return new ResultToReturnWithoutObject(false, errors);
    }
}