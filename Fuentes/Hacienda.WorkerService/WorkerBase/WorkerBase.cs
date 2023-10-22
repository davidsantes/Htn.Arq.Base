﻿using Serilog;
using System.Diagnostics.CodeAnalysis;

namespace Hacienda.WorkerService.WorkerBase
{
    /// <summary>
    /// Worker provides consistent logging (including a logger enriched with the type of the
    /// worker), and alerting. The <see cref="DoWorkAsync"/> method is called indefinetly, so long
    /// as it is supposed to.
    /// </summary>
    public abstract class WorkerBase : BackgroundService
    {
        private readonly IWorkerOptions workerOptions;

        protected WorkerBase(IWorkerOptions workerOptions)
        {
            this.workerOptions = workerOptions;

            WorkerName = GetType().Name;
            Logger = Log.ForContext("Type", WorkerName);
            Logger.Information(
                "Starting {worker}. Runs every {minutes} minutes. All options {@options}",
                WorkerName,
                this.workerOptions.RepeatIntervalSeconds,
                this.workerOptions);
        }

        public string WorkerName { get; }

        public Serilog.ILogger Logger { get; }

        /// <summary>
        /// Work method run based on <see cref="IWorkerOptions.RepeatIntervalSeconds"/>. Exceptions
        /// thrown here are turned into alerts.
        /// </summary>
        public abstract Task DoWorkAsync();

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
                        Logger.Information("Calling DoWorkAsync");
                        await DoWorkAsync().ConfigureAwait(false);
                    }
                    catch (Exception ex)
                    {
                        Logger.Error(
                            ex,
                            "Unhandled exception occurred in the {worker}. Sending an alert. Worker will retry after the normal interveral.",
                            WorkerName);
                    }

                    await Task.Delay(workerOptions.RepeatIntervalSeconds * 1000, stoppingToken).ConfigureAwait(false);
                }

                Logger.Information(
                    "Execution ended. Cancelation token cancelled = {IsCancellationRequested}",
                    stoppingToken.IsCancellationRequested);
            }
            catch (Exception ex) when (stoppingToken.IsCancellationRequested)
            {
                Logger.Warning(ex, "Execution Cancelled");
            }
            catch (Exception ex)
            {
                Logger.Error(ex, "Unhandled exception. Execution Stopping");
            }
        }
    }
}