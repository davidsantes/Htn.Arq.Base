using Htn.Infrastructure.Core.Exceptions.Entities;
using Htn.Infrastructure.Core.Exceptions.Policies.Interfaces;

namespace Htn.Infrastructure.Core.Exceptions.Policies.Imp
{
    public class SanitizeAllExceptionsPolicy : IExceptionPolicy
    {
        public Exception ApplyPolicy(Exception sourceException)
        {
            return new GenericException(sourceException);
        }
    }
}