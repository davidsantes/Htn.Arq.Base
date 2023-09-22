using HealthChecks.UI.Client;
using Htn.Arq.Base.WebApi.Application;
using Htn.Arq.Base.WebApi.Builder;
using Htn.Arq.Base.WebApi.HealthChecks;
using Htn.Arq.Base.WebApi.Middlewares;
using Htn.Infrastructure.Di;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();

//TODO: configurar API Swagger con options.SwaggerDoc, options.IncludeXmlComments...
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddSwaggerGen();

builder.Services.RegisterExceptionPolicies()
    .RegisterDalRepositories()
    .RegisterBllServices()
    .RegisterDtoValidators()
    .RegisterAutomapperProfiles();

builder.Services.AddHealthChecks()
    .AddCheck<MyCustomHealthCheck>("MyCustomHealthCheck");

builder.Host.UseSerilog((context, configuration) =>
    configuration.ReadFrom.Configuration(context.Configuration));

var app = builder.Build();
app.UseMiddleware<ExceptionHandlingMiddleware>();

if (app.Environment.IsDevelopment()
    || app.Environment.IsStaging()
    || app.Environment.IsGesValidacion())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseSerilogRequestLogging();

app.UseExceptionHandling();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

//Configuración de healthchecks con formato de API Rest, se podrá acceder a través de /_health
app.MapHealthChecks("/_health", new HealthCheckOptions
{
    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
});

//TODO: escribir un log para garantizar que la app funciona

app.Run();