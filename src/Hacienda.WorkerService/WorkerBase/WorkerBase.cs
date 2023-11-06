using System.Diagnostics.CodeAnalysis;

namespace Hacienda.WorkerService.WorkerBase;

/// <summary>
/// El Worker proporciona un registro consistente, incluyendo un logger enriquecido con el tipo del worker y alertas.
/// El método <see cref="DoWorkAsync"/> se llama indefinidamente.
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
            $"INICIO: background service: {WorkerName}. Se ejecuta cada {_workerOptions.RepeatIntervalInSeconds} segundos. Opciones: {_workerOptions}",
            WorkerName,
            _workerOptions.RepeatIntervalInSeconds,
            _workerOptions);
    }

    public string WorkerName { get; }

    /// <summary>
    /// Método basado en <see cref="IWorkerOptionsBase.RepeatIntervalInSeconds"/>.
    /// Las excepciones se lanzarán aquí y se convertirán en alertas.
    /// </summary>
    public abstract Task DoWorkAsync(IServiceScope scope);

    [SuppressMessage("Design", "CA1031:Do not catch general exception types", Justification = "We catch anything and alert instead of rethrowing")]
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        try
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    using var scope = _serviceScopeFactory.CreateScope();

                    _logger.LogInformation("INICIO: background service: {WorkerName}, hora: {time}, Llamando a DoWorkAsync", GetType().Name, DateTimeOffset.Now);

                    await DoWorkAsync(scope).ConfigureAwait(false);
                }
                catch (Exception ex)
                {
                    // Registra cualquier excepción no controlada y envía una alerta. El trabajador volverá a intentarlo después del intervalo normal.
                    _logger.LogError(
                        ex,
                        $"ERROR: Se produjo una excepción no controlada en el {WorkerName}.",
                        WorkerName);
                }

                // Espera un tiempo antes de volver a realizar la tarea.
                await Task
                    .Delay(_workerOptions.RepeatIntervalInSeconds * 1000, stoppingToken)
                    .ConfigureAwait(false);
            }

            // Registra el fin de la ejecución y si se canceló el token de cancelación.
            _logger.LogInformation(
                $"FIN: background service: {WorkerName}. Token de cancelación cancelado = {stoppingToken.IsCancellationRequested}",
                stoppingToken.IsCancellationRequested);
        }
        catch (Exception ex) when (stoppingToken.IsCancellationRequested)
        {
            // Registra una excepción si la ejecución se cancela.
            _logger.LogWarning(ex, $"ERROR: background service: {WorkerName}. Ejecución cancelada.");
        }
        catch (Exception ex)
        {
            // Registra una excepción no controlada si la ejecución se detiene.
            _logger.LogError(ex, $"ERROR: background service: {WorkerName}. Ejecución no controlada.");
        }
    }
}