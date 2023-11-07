using Hacienda.Application.DependencyInjection.Extensions;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();

builder.Services.RegisterExceptionAndProblemDetails()
    .RegisterRepositoriesSingleton()
    .RegisterAdaptersSingleton()
    .RegisterServicesSingleton()
    .RegisterRequestValidatorsTransient()
    .RegisterAutomapperProfiles();

//Prueba de concepto con acceso mediante Dapper a la base de datos
var databaseConnectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.RegisterDapper(databaseConnectionString);
builder.Services.RegisterEntityFrameworkTransient(builder.Configuration);

builder.Host.UseSerilog((context, configuration) =>
    configuration.ReadFrom.Configuration(context.Configuration));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

var logger = app.Services.GetRequiredService<ILogger<Program>>();
logger.LogInformation("La aplicaci�n se est� ejecutando...");

await app.RunAsync();