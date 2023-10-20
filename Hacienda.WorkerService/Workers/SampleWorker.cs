using Hacienda.Application.Contracts.Services;

namespace Hacienda.WorkerService.Workers
{
    public class SampleWorker : BackgroundService
    {
        private readonly ILogger<SampleWorker> _logger;
        private readonly ICategoriaProductoService _categoriaProductoService;

        public SampleWorker(ILogger<SampleWorker> logger
            , ICategoriaProductoService categoriaProductoService)
        {
            _logger = logger;
            _categoriaProductoService = categoriaProductoService;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation("INICIO: tarea programada: {time}", DateTimeOffset.Now);

                var categorias = await _categoriaProductoService.GetCategoriasProductoAsync();
                foreach (var categoria in categorias)
                {
                    _logger.LogInformation(categoria.Descripcion);
                }

                _logger.LogInformation("FIN: tarea programada: {time}", DateTimeOffset.Now);

                // Espera un tiempo antes de ejecutar la tarea nuevamente
                await Task.Delay(TimeSpan.FromMinutes(1), stoppingToken);
            }
        }
    }
}