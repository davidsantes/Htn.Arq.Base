using Hacienda.Domain.Exceptions.Base;

namespace Hacienda.Application.Exceptions.Sanitize;

/// <summary>
/// Política de saneamiento de excepciones.
/// </summary>
public class SanitizeNotControlledExceptionsPolicy : IExceptionPolicy
{
    /// <summary>
    /// Sanea las excepciones no controladas
    /// </summary>
    /// <param name="sourceException">Excepción generada</param>
    /// <returns>Devuelve GenericException excepto si es CustomException</returns>
    public Exception ApplyPolicy(Exception sourceException)
    {
        if (sourceException is NotFoundException
            || sourceException is OperationException)
        {
            return sourceException;
        }

        return new GenericGuidException(sourceException);
    }
}