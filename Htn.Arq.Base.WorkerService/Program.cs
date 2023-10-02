using Htn.Arq.Base.WorkerService.Workers;
using Htn.Infrastructure.Core.Layers;
using Htn.Infrastructure.Di;
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
            .RegisterDalRepositories(ProjectTypes.WorkerService)
            .RegisterDalAdapters(ProjectTypes.WorkerService)
            .RegisterBllServices(ProjectTypes.WorkerService);
        //services.AddHostedService<SampleWorker>();

        services.Configure<TimeFileWorkerOptions>(hostContext.Configuration.GetSection("TimeFileWorker"));
        services.AddHostedService<TimeFileWorker>();
        services.AddSingleton<ITimeService, TimeService>();
    })
    //TODO: utilizar un middleware de control de excepciones
    .Build();

await host.RunAsync();