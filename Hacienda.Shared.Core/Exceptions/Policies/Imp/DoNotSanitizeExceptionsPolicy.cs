using Hacienda.Shared.Core.Exceptions.Policies.Interfaces;

namespace Hacienda.Shared.Core.Exceptions.Policies.Imp
{
    /// <summary>
    /// Política de saneamiento de excepciones.
    /// </summary>
    public class DoNotSanitizeExceptionsPolicy : IExceptionPolicy
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
}