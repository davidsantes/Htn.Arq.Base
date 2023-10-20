using Hacienda.Shared.Core.Layers;
using Hacienda.Shared.DependencyInjection;
using Hacienda.WorkerService.Workers;
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
            .RegisterRepositories(ProjectTypes.WorkerService)
            .RegisterAdapters(ProjectTypes.WorkerService)
            .RegisterServices(ProjectTypes.WorkerService)   
            .RegisterRequestValidators()
            .RegisterAutomapperProfiles();
        //services.AddHostedService<SampleWorker>();

        services.Configure<TimeFileWorkerOptions>(hostContext.Configuration.GetSection("TimeFileWorker"));
        services.AddHostedService<TimeFileWorker>();
        services.AddSingleton<ITimeService, TimeService>();
    })
    //TODO: utilizar un middleware de control de excepciones
    .Build();

await host.RunAsync();