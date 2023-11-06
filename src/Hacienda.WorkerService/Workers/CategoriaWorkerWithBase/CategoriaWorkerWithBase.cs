using Hacienda.Application.Services;
using Hacienda.WorkerService.Workers.CategoriaWorkerWithBase.Settings;
using Microsoft.Extensions.Options;

namespace Hacienda.WorkerService.Workers.CategoriaWorkerWithBase;

public class CategoriaWorkerWithBase : WorkerBase.WorkerBase
{
    private readonly CategoriaWorkerOptions _workerOptions;
    private readonly ILogger<CategoriaWorkerWithBase> _logger;

    public CategoriaWorkerWithBase(
        IServiceScopeFactory serviceScopeFactory
        , IOptions<CategoriaWorkerOptions> workerOptions
        , ILogger<CategoriaWorkerWithBase> logger)
        : base(serviceScopeFactory
            , workerOptions.Value
            , logger)
    {
        _workerOptions = workerOptions.Value;
        _logger = logger;
    }

    public override async Task DoWorkAsync(IServiceScope scope)
    {
        var _categoriaProductoService = scope.ServiceProvider.GetService<ICategoriaProductoService>();

        await GetCategorias(_categoriaProductoService);
    }

    private async Task GetCategorias(ICategoriaProductoService categoriaProductoService)
    {
        var categorias = await categoriaProductoService.GetAllAsync();
        foreach (var categoria in categorias)
        {
            _logger.LogInformation($"Id: {categoria.Id}, Nombre: {categoria.Nombre}");
        }
    }
}