using Hacienda.Application.Services;
using Hacienda.WorkerService.Workers.CategoriaWorkerWithBase.Settings;
using Microsoft.Extensions.Options;

namespace Hacienda.WorkerService.Workers.CategoriaWorkerWithoutBase;

public class CategoriaWorkerWithoutBase : BackgroundService
{
    private readonly IServiceScopeFactory _serviceScopeFactory;
    private readonly CategoriaWorkerOptionsWithoutBase _workerOptions;

    public CategoriaWorkerWithoutBase(
        IServiceScopeFactory serviceScopeFactory
        , IOptions<CategoriaWorkerOptionsWithoutBase> workerOptions)
    {
        _serviceScopeFactory = serviceScopeFactory;
        _workerOptions = workerOptions.Value;
        WorkerName = GetType().Name;
    }

    public string WorkerName { get; }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            using var scope = _serviceScopeFactory.CreateScope();

            var logger = scope.ServiceProvider.GetService<ILogger<CategoriaWorkerWithoutBase>>();
            var categoriaProductoService = scope.ServiceProvider.GetService<ICategoriaProductoService>();
            
            logger.LogInformation("INICIO: background service: {WorkerName}, hora: {time}", GetType().Name, DateTimeOffset.Now);

            var categorias = await categoriaProductoService.GetAllAsync();
            foreach (var categoria in categorias)
            {
                logger.LogInformation(categoria.Nombre);
            }

            logger.LogInformation("FIN: background service: {WorkerName}, hora: {time}", GetType().Name, DateTimeOffset.Now);

            await Task.Delay(TimeSpan.FromSeconds(_workerOptions.RepeatIntervalInSeconds), stoppingToken);
        }
    }
}