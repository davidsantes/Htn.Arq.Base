using Htn.Arq.Base.WebApi.Start;
using Htn.Infrastructure.Di;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.RegisterDalRepositories()
    .RegisterBllServices()
    .RegisterAutomapperProfiles()
    .RegisterDtoValidators();

 var app = builder.Build();

if (app.Environment.IsDevelopment() 
    || app.Environment.IsStaging() 
    || app.Environment.IsGesValidacion())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();