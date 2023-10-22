namespace Hacienda.Domain.Entities.Exceptions
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
                return string.Format(ExceptionConstants.SanitizedExceptionMessage, Guid);
            }
        }

        public override ExceptionPriority Priority
        {
            get
            {
                return ExceptionPriority.Low;
            }
        }

        public override string ToString()
        {
            return "Exception GUID: " + Guid + Environment.NewLine + base.ToString();
        }
    }
}