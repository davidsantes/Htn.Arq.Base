using Htn.Infrastructure.Core.Logging.Entities;

namespace Htn.Infrastructure.Core.Exceptions.Entities
{
    /// <summary>
    /// Excepción genérica con un guid de seguimiento.
    /// Envuelve una excepción estándar.
    /// </summary>
    [Serializable]
    public class GenericException : AbstractCustomException
    {
        public Guid Guid { get; set; }

        public GenericException(Exception exception) : base(exception)
        {
            Guid = Guid.NewGuid();
        }

        public override string Message
        {
            get
            {
                return String.Format(ExceptionConstants.SanitizedExceptioMessage, Guid);
            }
        }

        public override Priority Priority
        {
            get
            {
                return Priority.Low;
            }
        }

        public override string ToString()
        {
            return "Exception GUID: " + Guid + Environment.NewLine + base.ToString();
        }
    }
}