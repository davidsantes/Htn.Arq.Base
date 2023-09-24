using Htn.Arq.Base.Bll.Services.Interfaces;

namespace Htn.Arq.Base.WorkerService
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly ICategoriaProductoService _categoriaProductoService;

        public Worker(ILogger<Worker> logger
            , ICategoriaProductoService categoriaProductoService)
        {
            _logger = logger;
            _categoriaProductoService = categoriaProductoService;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation("Inicio: tarea programada: {time}", DateTimeOffset.Now);

                var categorias = await _categoriaProductoService.GetCategoriasProductoAsync();
                foreach (var categoria in categorias)
                {
                    _logger.LogInformation(categoria.Descripcion);
                }

                _logger.LogInformation("Inicio: fin programada: {time}", DateTimeOffset.Now);

                await Task.Delay(1000, stoppingToken);
            }
        }
    }
}