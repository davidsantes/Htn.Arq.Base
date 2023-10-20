namespace Hacienda.Shared.Core.Exceptions.Policies.Interfaces
{
    /// <summary>
    /// Política de saneamiento de excepciones.
    /// </summary>
    public interface IExceptionPolicy
    {
        Exception ApplyPolicy(Exception sourceException);
    }
}