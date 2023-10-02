using HealthChecks.UI.Client;
using Htn.Arq.Base.WebApi.Builder;
using Htn.Arq.Base.WebApi.HealthChecks;
using Htn.Arq.Base.WebApi.RegisterExtensions;
using Htn.Arq.Base.WebApi.Resources;
using Htn.Infrastructure.Core.Layers;
using Htn.Infrastructure.Di;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddCustomSwagger();

builder.Services.RegisterExceptionAndProblemDetails()
    .RegisterDalRepositories(ProjectTypes.WebApi)
    .RegisterDalAdapters(ProjectTypes.WebApi)
    .RegisterBllServices(ProjectTypes.WebApi)
    .RegisterDtoValidators()
    .RegisterAutomapperProfiles();

builder.Services.AddHealthChecks()
    .AddCheck<MyCustomHealthCheck>("MyCustomHealthCheck");

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

//Configuraci�n de healthchecks con formato de API Rest, se podr� acceder a trav�s de /_health
app.MapHealthChecks("/_health", new HealthCheckOptions
{
    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
});

var logger = app.Services.GetRequiredService<ILogger<Program>>();
logger.LogInformation("La aplicaci�n se est� ejecutando...");

await app.RunAsync();