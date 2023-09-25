using Htn.Arq.Base.WorkerService;
using Htn.Infrastructure.Core.Layers;
using Htn.Infrastructure.Di;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(services =>
    {
        services.RegisterExceptionPolicies()
            .RegisterDalRepositories(ProjectTypes.WorkerService)
            .RegisterDalAdapters(ProjectTypes.WorkerService)
            .RegisterBllServices(ProjectTypes.WorkerService);
        services.AddHostedService<Worker>();
    })
    .Build();

await host.RunAsync();