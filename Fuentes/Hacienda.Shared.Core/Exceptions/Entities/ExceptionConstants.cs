namespace Hacienda.Shared.Core.Exceptions.Entities
{
    public class ExceptionConstants
    {
        public const string ContentTypeJson = "application/json";

        public const string SanitizedExceptioMessage = "Ha ocurrido un error con el siguiente código de error: {0}. Para más detalles consulte con su administrador.";
    }

    public class ExceptionConstantsTypes
    {
        public const string ExceptionTypeValidationFailure = "ValidationFailure";

        public const string ExceptionTypeUnexpectedException = "UnexpectedException";

        public const string ExceptionTypeNotFound = "ResourceNotFound";

        public const string ExceptionTypeControlledInBackend = "ExceptionTypeControlledInBackend";
    }
}