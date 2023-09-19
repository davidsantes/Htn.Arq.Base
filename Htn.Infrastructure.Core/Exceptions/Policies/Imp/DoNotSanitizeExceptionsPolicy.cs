using Htn.Infrastructure.Core.Exceptions.Policies.Interfaces;

namespace Htn.Infrastructure.Core.Exceptions.Policies.Imp
{
    public class DoNotSanitizeExceptionsPolicy : IExceptionPolicy
    {
        public Exception ApplyPolicy(Exception sourceException)
        {
            return sourceException;
        }
    }
}