namespace Hacienda.Domain.Exceptions.Generic;

public class NotFoundException : Exception
{
    public NotFoundException()
    {
    }

    public NotFoundException(string message)
        : base(message)
    {
    }
}