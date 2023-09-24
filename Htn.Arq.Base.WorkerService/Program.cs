using Htn.Arq.Base.WorkerService;
using Htn.Infrastructure.Di;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(services =>
    {
        var isSingleton = true;
        services.RegisterExceptionPolicies()
            .RegisterDalRepositories(isSingleton)
            .RegisterBllServices(isSingleton);
        services.AddHostedService<Worker>();
    })
    .Build();

await host.RunAsync();