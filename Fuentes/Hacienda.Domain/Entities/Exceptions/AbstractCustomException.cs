using System.Runtime.Serialization;

namespace Hacienda.Domain.Entities.Exceptions
{
    [Serializable]
    public abstract class AbstractCustomException : Exception
    {
        public abstract ExceptionPriority Priority
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