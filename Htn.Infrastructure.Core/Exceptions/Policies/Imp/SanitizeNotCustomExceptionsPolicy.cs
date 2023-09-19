using Htn.Infrastructure.Core.Exceptions.Entities;
using Htn.Infrastructure.Core.Exceptions.Policies.Interfaces;

namespace Htn.Infrastructure.Core.Exceptions.Policies.Imp
{
    public class SanitizeNotCustomExceptionsPolicy : IExceptionPolicy
    {
        public Exception ApplyPolicy(Exception sourceException)
        {
            if (!(sourceException is CustomException))
            {
                return new GenericException(sourceException);
            }

            return sourceException;
        }
    }
}