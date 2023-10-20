using Hacienda.Shared.Core.Exceptions.Entities;
using Hacienda.Shared.Core.Exceptions.Policies.Interfaces;

namespace Hacienda.Shared.Core.Exceptions.Policies.Imp
{
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
}