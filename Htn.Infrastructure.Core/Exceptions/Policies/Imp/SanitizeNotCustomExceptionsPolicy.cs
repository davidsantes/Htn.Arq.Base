using Htn.Infrastructure.Core.Exceptions.Entities;
using Htn.Infrastructure.Core.Exceptions.Policies.Interfaces;

namespace Htn.Infrastructure.Core.Exceptions.Policies.Imp
{
    /// <summary>
    /// Política de saneamiento de excepciones.
    /// </summary>
    public class SanitizeNotCustomExceptionsPolicy : IExceptionPolicy
    {
        /// <summary>
        /// Sanea todo excepto CustomException
        /// </summary>
        /// <param name="sourceException">Excepción generada</param>
        /// <returns>Devuelve GenericException excepto si es CustomException</returns>
        public Exception ApplyPolicy(Exception sourceException)
        {
            if (!(sourceException is CustomException))
            {
                return new GenericException(sourceException);
            }

            return sourceException;
        }
    }
}