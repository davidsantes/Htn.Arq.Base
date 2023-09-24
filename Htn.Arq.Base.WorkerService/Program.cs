using Htn.Arq.Base.WorkerService;
using Htn.Infrastructure.Di;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(services =>
    {
        services.RegisterExceptionPolicies()
            .RegisterDalRepositories()
            .RegisterBllServices();
        services.AddHostedService<Worker>();
    })
    .Build();

await host.RunAsync();