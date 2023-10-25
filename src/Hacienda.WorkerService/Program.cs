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
        //services.AddHostedService<SampleWorker>();

        //Prueba de concepto con acceso mediante Dapper a la base de datos:
        services
            .RegisterDapper(hostContext.Configuration.GetConnectionString("DefaultConnection"));

        services.Configure<TimeFileWorkerOptions>(hostContext.Configuration.GetSection("TimeFileWorker"));
        services.AddHostedService<TimeFileWorker>();
        services.AddSingleton<ITimeService, TimeService>();
    })
    //TODO: ver c�mo controlar las excepciones
    .Build();

await host.RunAsync();