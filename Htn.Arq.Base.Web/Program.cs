using Htn.Infrastructure.Core.Layers;
using Htn.Infrastructure.Di;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();

builder.Services.RegisterExceptionAndProblemDetails()
    .RegisterDalRepositories(ProjectTypes.WebBlazorServer)
    .RegisterDalAdapters(ProjectTypes.WebBlazorServer)
    .RegisterBllServices(ProjectTypes.WebBlazorServer);

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

await app.RunAsync();