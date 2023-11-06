using Hacienda.WorkerService.WorkerBase;

namespace Hacienda.WorkerService.Workers.CategoriaWorkerWithBase.Settings;

public class CategoriaWorkerOptionsWithoutBase : IWorkerOptionsBase
{
    /// <inheritdoc/>
    public int RepeatIntervalInSeconds { get; set; }
}