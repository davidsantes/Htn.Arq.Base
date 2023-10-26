namespace Hacienda.Domain.Exceptions.Base;

public class OperationException : Exception
{
    public OperationException()
    {
    }

    public OperationException(string message)
        : base(message)
    {
    }
}