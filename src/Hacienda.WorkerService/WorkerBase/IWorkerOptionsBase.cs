namespace Hacienda.WorkerService.WorkerBase;

public interface IWorkerOptionsBase
{
    /// <summary>
    /// Defines the period between the end of one unit of work and the start of the next.
    /// </summary>
    int RepeatIntervalSeconds { get; set; }
}