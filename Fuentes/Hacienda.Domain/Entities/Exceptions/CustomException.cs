using System.Runtime.Serialization;

namespace Hacienda.Domain.Entities.Exceptions
{
    /// <summary>
    /// Excepción personalizada. Útil para enviar excepciones controladas
    /// </summary>
    [Serializable]
    public class CustomException : AbstractCustomException
    {
        public override ExceptionPriority Priority
        {
            get
            {
                return ExceptionPriority.Medium;
            }
        }

        public CustomException()
            : base()
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
    }
}