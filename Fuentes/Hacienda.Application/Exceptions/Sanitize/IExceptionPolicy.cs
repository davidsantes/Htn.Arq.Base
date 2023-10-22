namespace Hacienda.Application.Exceptions.Sanitize
{
    /// <summary>
    /// Política de saneamiento de excepciones.
    /// </summary>
    public interface IExceptionPolicy
    {
        Exception ApplyPolicy(Exception sourceException);
    }
}