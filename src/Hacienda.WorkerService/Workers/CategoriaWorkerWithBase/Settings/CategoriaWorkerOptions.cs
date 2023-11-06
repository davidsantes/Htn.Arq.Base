using Hacienda.WorkerService.WorkerBase;

namespace Hacienda.WorkerService.Workers.CategoriaWorkerWithBase.Settings;

public class CategoriaWorkerOptions : IWorkerOptionsBase
{
    /// <inheritdoc/>
    public int RepeatIntervalInSeconds { get; set; }

    /// <summary>
    /// Se podrían añadir x opciones
    /// </summary>
    public string MockOption { get; set; }
}