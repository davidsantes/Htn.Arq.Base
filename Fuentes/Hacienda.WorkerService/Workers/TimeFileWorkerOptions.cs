using Hacienda.WorkerService.WorkerBase;

namespace Hacienda.WorkerService.Workers
{
    public class TimeFileWorkerOptions : IWorkerOptions
    {
        /// <inheritdoc/>
        public int RepeatIntervalSeconds { get; set; }

        /// <summary>
        /// Output directory for the <see cref="TimeFileWorker"/>.
        /// </summary>
        public string OutputDirectory { get; set; }
    }
}