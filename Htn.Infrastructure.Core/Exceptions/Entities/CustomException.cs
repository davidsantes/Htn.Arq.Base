using Htn.Infrastructure.Core.Logging.Entities;
using System.Runtime.Serialization;

namespace Htn.Infrastructure.Core.Exceptions.Entities
{
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