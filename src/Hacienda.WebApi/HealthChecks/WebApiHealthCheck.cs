using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace Hacienda.WebApi.HealthChecks;

/// <summary>
/// Acceso al health check definido en MapHealthChecks
/// https://localhost:xxx/_health
/// </summary>
public class WebApiHealthCheck : IHealthCheck
{
    private readonly string _databaseConnectionString;

    public WebApiHealthCheck(string databaseConnectionString)
    {
        _databaseConnectionString = databaseConnectionString;
    }

    public async Task<HealthCheckResult> CheckHealthAsync(
        HealthCheckContext context,
        CancellationToken cancellationToken = default)
    {
        HealthCheckResult databaseCheckResult = await CheckDatabase(cancellationToken);
        HealthCheckResult genericServiceCheckResult = await CheckGenericService(cancellationToken);

        var message = $"databaseCheckResult.Status: {databaseCheckResult.Status}" +
            $", genericServiceCheckResult.Status: {genericServiceCheckResult.Status}";

        return new HealthCheckResult(
            (databaseCheckResult.Status == HealthStatus.Healthy
            && genericServiceCheckResult.Status == HealthStatus.Healthy) ? HealthStatus.Healthy : HealthStatus.Unhealthy,
            message);
    }

    private async Task<HealthCheckResult> CheckDatabase(CancellationToken cancellationToken)
    {
        try
        {
            using (var connection = new SqlConnection(_databaseConnectionString))
            {
                await connection.OpenAsync(cancellationToken);
                return HealthCheckResult.Healthy();
            }
        }
        catch
        {
            return new HealthCheckResult(HealthStatus.Unhealthy);
        }
    }

    private async Task<HealthCheckResult> CheckGenericService(CancellationToken cancellationToken)
    {
        try
        {
            return HealthCheckResult.Healthy();
        }
        catch
        {
            return new HealthCheckResult(HealthStatus.Unhealthy);
        }
    }
}