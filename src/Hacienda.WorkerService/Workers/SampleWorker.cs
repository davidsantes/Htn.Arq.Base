using Hacienda.Application.Services;

namespace Hacienda.WorkerService.Workers;

public class SampleWorker : BackgroundService
{
    private readonly IServiceScopeFactory _serviceScopeFactory;

    public SampleWorker(IServiceScopeFactory serviceScopeFactory)
    {
        _serviceScopeFactory = serviceScopeFactory;
    }
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            using var scope = _serviceScopeFactory.CreateScope();


            var logger = scope.ServiceProvider.GetService<ILogger<SampleWorker>>();
            var categoriaProductoService = scope.ServiceProvider.GetService<ICategoriaProductoService>();

            logger.LogInformation("INICIO: tarea programada: {time}", DateTimeOffset.Now);

            var categorias = await categoriaProductoService.GetAllAsync();
            foreach (var categoria in categorias)
            {
                logger.LogInformation(categoria.Nombre);
            }

            logger.LogInformation("FIN: tarea programada: {time}", DateTimeOffset.Now);

            // Espera un tiempo antes de ejecutar la tarea nuevamente
            await Task.Delay(TimeSpan.FromMinutes(1), stoppingToken);
        }
    }
}