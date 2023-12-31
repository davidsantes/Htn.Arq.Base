﻿namespace Hacienda.Application.Exceptions.Sanitize;

/// <summary>
/// Política de saneamiento de excepciones. No sanea ninguna excepción
/// </summary>
public class SanitizeNothingExceptionsPolicy : IExceptionPolicy
{
    /// <summary>
    /// No sanea ninguna excepción, por lo que las devuelve en crudo
    /// </summary>
    /// <param name="sourceException">Excepción generada</param>
    /// <returns>Devuelve la misma excepción</returns>
    public Exception ApplyPolicy(Exception sourceException)
    {
        return sourceException;
    }
}