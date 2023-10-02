using Htn.Infrastructure.Core.WorkerService;

namespace Htn.Arq.Base.WorkerService.Workers
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