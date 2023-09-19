using Htn.Infrastructure.Core.Logging.Entities;
using System.Runtime.Serialization;

namespace Htn.Infrastructure.Core.Exceptions.Entities
{
    [Serializable]
    public abstract class AbstractCustomException : Exception
    {
        public abstract Priority Priority
        {
            get;
        }

        protected AbstractCustomException()
        {
        }

        protected AbstractCustomException(string message)
            : base(message)
        {
        }

        protected AbstractCustomException(Exception exception)
            : base(exception.Message, exception)
        {
        }

        protected AbstractCustomException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}