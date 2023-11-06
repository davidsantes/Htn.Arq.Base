using Hacienda.Application.DependencyInjection.Extensions;
using Hacienda.WorkerService.Workers.CategoriaWorkerWithBase;
using Hacienda.WorkerService.Workers.CategoriaWorkerWithBase.Settings;
using Hacienda.WorkerService.Workers.CategoriaWorkerWithoutBase;
using Serilog;

IHost host = Host.CreateDefaultBuilder(args)
    .UseSerilog((context, _, configuration) =>
        configuration.ReadFrom.Configuration(context.Configuration))
    .ConfigureLogging((ctx, builder) =>
    {
        builder.ClearProviders();
        builder.AddSerilog();
    })
    .ConfigureServices((hostContext, services) =>
    {
        services.RegisterExceptionAndProblemDetails()
            .RegisterRepositories()
            .RegisterAdapters()
            .RegisterServices()
            .RegisterRequestValidators()
            .RegisterAutomapperProfiles();

        var databaseConnectionString = hostContext.Configuration.GetConnectionString("DefaultConnection");
        services.RegisterDapper(databaseConnectionString);
        services.RegisterEntityFramework(hostContext.Configuration);

        //Configuraci�n de CategoriaWorkerWithoutBase
        services.Configure<CategoriaWorkerOptionsWithoutBase>(hostContext.Configuration.GetSection("CategoriaWorkerWithoutBaseOptions"));
        services.AddHostedService<CategoriaWorkerWithoutBase>();

        //Configuraci�n de CategoriaWorkerWithBase
        services.Configure<CategoriaWorkerOptions>(hostContext.Configuration.GetSection("CategoriaWorkerWithBaseOptions"));
        services.AddHostedService<CategoriaWorkerWithBase>();
    })
    //TODO: ver c�mo controlar las excepciones
    .Build();

await host.RunAsync();