using Hacienda.Shared.Core.Logging.Entities;
using System.Runtime.Serialization;

namespace Hacienda.Shared.Core.Exceptions.Entities
{
    /// <summary>
    /// Excepción personalizada. Útil para enviar excepciones controladas
    /// </summary>
    [Serializable]
    public class CustomException : AbstractCustomException
    {
        public override Priority Priority
        {
            get
            {
                return Priority.Medium;
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