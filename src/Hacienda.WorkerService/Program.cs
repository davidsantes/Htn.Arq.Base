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

        //TODO: no funciona EF en un service:
        //https://stackoverflow.com/questions/36332239/use-dbcontext-in-asp-net-singleton-injected-class
        //https://www.youtube.com/watch?v=7lQ1fmR6LLE

#pragma warning disable S125 // Sections of code should not be commented out
        //services.AddHostedService<SampleWorker>();
#pragma warning restore S125 // Sections of code should not be commented out

        var databaseConnectionString = hostContext.Configuration.GetConnectionString("DefaultConnection");
        services.RegisterDapper(databaseConnectionString);
        services.RegisterEntityFramework();

        services.Configure<TimeFileWorkerOptions>(hostContext.Configuration.GetSection("TimeFileWorker"));
        services.AddHostedService<TimeFileWorker>();
        services.AddSingleton<ITimeService, TimeService>();
    })
    //TODO: ver cómo controlar las excepciones
    .Build();

await host.RunAsync();