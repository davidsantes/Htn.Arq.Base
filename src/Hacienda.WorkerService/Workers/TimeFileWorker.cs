using Hacienda.Application.Services;
using Microsoft.Extensions.Options;

namespace Hacienda.WorkerService.Workers;

public class TimeFileWorker : WorkerBase.WorkerBase
{
    private readonly TimeFileWorkerOptions _workerOptions;
    private readonly ILogger<TimeFileWorker> _logger;
    private readonly ITimeService _timeService;

    public TimeFileWorker(
        IServiceScopeFactory serviceScopeFactory
        , IOptions<TimeFileWorkerOptions> workerOptions
        , ILogger<TimeFileWorker> logger
        , ITimeService timeService)
        : base(serviceScopeFactory
            , workerOptions.Value
            , logger)
    {
        _workerOptions = workerOptions.Value;
        _timeService = timeService;
        _logger = logger;
    }

    public override async Task DoWorkAsync(IServiceScope scope)
    {
        var _categoriaProductoService = scope.ServiceProvider.GetService<ICategoriaProductoService>();

        await InsFile();
        await InsCategorias(_categoriaProductoService);
    }

    private async Task InsFile()
    {
        Directory.CreateDirectory(_workerOptions.OutputDirectory);
        var time = _timeService.GetDateTime();
        var outFile = Path.Combine(
            _workerOptions.OutputDirectory,
            $"{time:yyyy-MM-dd--HHmmss}.txt");
        await File.WriteAllTextAsync(outFile, WorkerName);
        _logger.LogInformation("outFile: {outFile}", outFile);
    }

    private async Task InsCategorias(ICategoriaProductoService categoriaProductoService)
    {
        var categorias = await categoriaProductoService.GetAllAsync();
        foreach (var categoria in categorias)
        {
            _logger.LogInformation(categoria.Nombre);
        }
    }
}