namespace Hacienda.WorkerService.WorkerBase;

/// <summary>
/// Valores base que deben cumplir todos los background service que utilicen WorkerBase
/// </summary>
public interface IWorkerOptionsBase
{
    /// <summary>
    /// Define el periodo entre una ejecución y otra
    /// </summary>
    int RepeatIntervalInSeconds { get; set; }
}