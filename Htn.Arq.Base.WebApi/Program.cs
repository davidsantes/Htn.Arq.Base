using HealthChecks.UI.Client;
using Htn.Arq.Base.WebApi.Application;
using Htn.Arq.Base.WebApi.Builder;
using Htn.Arq.Base.WebApi.HealthChecks;
using Htn.Arq.Base.WebApi.Middlewares;
using Htn.Infrastructure.Di;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;

var builder = WebApplication.CreateBuilder(args);

builder.Services.RegisterException()
    .RegisterMiddlewares();

builder.Services.AddControllers();

//TODO: configurar API Swagger
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.RegisterDalRepositories()
    .RegisterBllServices()    
    .RegisterDtoValidators()
    .RegisterAutomapperProfiles();

builder.Services.AddHealthChecks()
    .AddCheck<MyCustomHealthCheck>("MyCustomHealthCheck");

var app = builder.Build();
app.UseMiddleware<ExceptionHandlingMiddleware>();

if (app.Environment.IsDevelopment()
    || app.Environment.IsStaging()
    || app.Environment.IsGesValidacion())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseExceptionHandling();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

//Configuración de healthchecks con formato de API Rest, se podrá acceder a través de /_health
app.MapHealthChecks("/_health", new HealthCheckOptions
{
    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
});

app.Run();