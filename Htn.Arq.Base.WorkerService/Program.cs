using Htn.Arq.Base.WorkerService;
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
    .ConfigureServices(services =>
    {
        services.RegisterExceptionPolicies()
            .RegisterDalRepositories(ProjectTypes.WorkerService)
            .RegisterDalAdapters(ProjectTypes.WorkerService)
            .RegisterBllServices(ProjectTypes.WorkerService);
        services.AddHostedService<Worker>();
    })
    //TODO: utilizar un middleware de control de excepciones
    .Build();

await host.RunAsync();