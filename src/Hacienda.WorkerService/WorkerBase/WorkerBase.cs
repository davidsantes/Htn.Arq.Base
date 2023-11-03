using System.Diagnostics.CodeAnalysis;

namespace Hacienda.WorkerService.WorkerBase;

/// <summary>
/// Worker provides consistent logging (including a logger enriched with the type of the
/// worker), and alerting. The <see cref="DoWorkAsync"/> method is called indefinetly, so long
/// as it is supposed to.
/// </summary>
public abstract class WorkerBase : BackgroundService
{
    private readonly IServiceScopeFactory _serviceScopeFactory;
    private readonly IWorkerOptionsBase _workerOptions;
    private readonly ILogger _logger;

    protected WorkerBase(IServiceScopeFactory serviceScopeFactory
        , IWorkerOptionsBase workerOptions
        , ILogger logger)
    {
        _serviceScopeFactory = serviceScopeFactory;
        _workerOptions = workerOptions;
        _logger = logger;

        WorkerName = GetType().Name;

        _logger.LogInformation(
            "Starting {worker}. Runs every {minutes} minutes. All options {@options}",
            WorkerName,
            _workerOptions.RepeatIntervalSeconds,
            _workerOptions);
    }

    public string WorkerName { get; }

    /// <summary>
    /// Work method run based on <see cref="IWorkerOptionsBase.RepeatIntervalSeconds"/>. Exceptions
    /// thrown here are turned into alerts.
    /// </summary>
    public abstract Task DoWorkAsync(IServiceScope scope);

    [SuppressMessage("Design", "CA1031:Do not catch general exception types", Justification = "We catch anything and alert instead of rethrowing")]
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        // Await right away so Host Startup can continue.
        await Task.Delay(10).ConfigureAwait(false);

        try
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    using var scope = _serviceScopeFactory.CreateScope();

                    _logger.LogInformation("Calling DoWorkAsync");
                    await DoWorkAsync(scope).ConfigureAwait(false);
                }
                catch (Exception ex)
                {
                    _logger.LogError(
                        ex,
                        "Unhandled exception occurred in the {worker}. Sending an alert. Worker will retry after the normal interveral.",
                        WorkerName);
                }

                await Task.Delay(_workerOptions.RepeatIntervalSeconds * 1000, stoppingToken)
                    .ConfigureAwait(false);
            }

            _logger.LogInformation(
                "Execution ended. Cancelation token cancelled = {IsCancellationRequested}",
                stoppingToken.IsCancellationRequested);
        }
        catch (Exception ex) when (stoppingToken.IsCancellationRequested)
        {
            _logger.LogWarning(ex, "Execution Cancelled");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Unhandled exception. Execution Stopping");
        }
    }
}