using System.Runtime.Serialization;

namespace Hacienda.Domain.Exceptions.Generic;

public class OperationWithoutEffectException : Exception
{
    public OperationWithoutEffectException()
    {
    }

    public OperationWithoutEffectException(string message)
        : base(message)
    {
    }
}