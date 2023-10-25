using Hacienda.Shared.DependencyInjection;
using Hacienda.Shared.DependencyInjection.Projects;
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

        //TODO: no funciona EF en un service https://stackoverflow.com/questions/36332239/use-dbcontext-in-asp-net-singleton-injected-class

        //services.AddHostedService<SampleWorker>();

        var databaseConnectionString = hostContext.Configuration.GetConnectionString("DefaultConnection");
        services.RegisterDapper(databaseConnectionString);
        services.RegisterEntityFramework(databaseConnectionString);

        services.Configure<TimeFileWorkerOptions>(hostContext.Configuration.GetSection("TimeFileWorker"));
        services.AddHostedService<TimeFileWorker>();
        services.AddSingleton<ITimeService, TimeService>();
    })
    //TODO: ver cómo controlar las excepciones
    .Build();

await host.RunAsync();