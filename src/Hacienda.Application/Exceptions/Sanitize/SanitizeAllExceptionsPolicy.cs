using Hacienda.Domain.Exceptions.Generic;

namespace Hacienda.Application.Exceptions.Sanitize;

/// <summary>
/// Política de saneamiento de excepciones.
/// </summary>
public class SanitizeAllExceptionsPolicy : IExceptionPolicy
{
    /// <summary>
    /// Sanea todo
    /// </summary>
    /// <param name="sourceException">Excepción generada</param>
    /// <returns>Devuelve GenericException</returns>
    public Exception ApplyPolicy(Exception sourceException)
    {
        return new GenericException(sourceException);
    }
}