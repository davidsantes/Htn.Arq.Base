namespace Htn.Infrastructure.Core.Exceptions.Policies.Interfaces
{
    public interface IExceptionPolicy
    {
        Exception ApplyPolicy(Exception sourceException);
    }
}