using System.Runtime.Serialization;

namespace Hacienda.Domain.Exceptions.Generic;

/// <summary>
/// Excepción personalizada. Útil para enviar excepciones controladas
/// </summary>
[Serializable]
public class CustomException : Exception
{
    public CustomException()
    {
    }

    public CustomException(string message)
        : base(message)
    {
    }

    public CustomException(SerializationInfo info, StreamingContext context)
        : base(info, context)
    {
    }

    public override string Message => "Ha ocurrido un error personalizado.";
}
