namespace Htn.Arq.Base.WebApi.Builder
{
    public static class EnvironmentExtensions
    {
        private const string GesValidacionEnvironmentName = "GESVAL";

        public static bool IsGesValidacion(this IHostEnvironment Environment)
        {
            return GesValidacionEnvironmentName.Equals(Environment.EnvironmentName, StringComparison.InvariantCultureIgnoreCase);
        }
    }
}
