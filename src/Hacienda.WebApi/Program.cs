using Hacienda.Shared.DependencyInjection;
using Hacienda.Shared.DependencyInjection.Projects;
using Hacienda.WebApi.Builder;
using Hacienda.WebApi.HealthChecks;
using Hacienda.WebApi.RegisterExtensions;
using Hacienda.WebApi.Resources;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddCustomSwagger();

builder.Services.RegisterExceptionAndProblemDetails()
    .RegisterRepositories(ProjectTypes.WebApi)
    .RegisterAdapters(ProjectTypes.WebApi)
    .RegisterServices(ProjectTypes.WebApi)
    .RegisterRequestValidators()
    .RegisterAutomapperProfiles();

var databaseConnectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.RegisterDapper(databaseConnectionString);
builder.Services.RegisterEntityFramework();

builder.Services.AddSingleton<WebApiHealthCheck>(new WebApiHealthCheck(databaseConnectionString));
builder.Services.AddHealthChecks()
    .AddCheck<WebApiHealthCheck>("WebApiHealthCheck");

builder.Host.UseSerilog((context, configuration) =>
    configuration.ReadFrom.Configuration(context.Configuration));

var app = builder.Build();

if (app.Environment.IsDevelopment()
    || app.Environment.IsStaging()
    || app.Environment.IsGesValidacion())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
        options.DocumentTitle = WebApiResources.WebApi_Titulo;
    });
}

app.UseSerilogRequestLogging();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.UseExceptionHandling();

//Configuración de healthchecks con formato de API Rest, se podrá acceder a través de /_health
app.MapHealthChecks("/_health", new HealthCheckOptions
{
    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
});

var logger = app.Services.GetRequiredService<ILogger<Program>>();
logger.LogInformation("La aplicación se está ejecutando...");

await app.RunAsync();