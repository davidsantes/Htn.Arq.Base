using Hacienda.WebApi.Resources;
using Microsoft.OpenApi.Models;
using System.Reflection;

namespace Hacienda.WebApi.RegisterExtensions;

public static class SwaggerExtension
{
    /// <summary>
    /// Registra la configuración de Swagger enriqueciendo sus opciones por defecto.
    /// Incluye:
    /// - Que se muestre información extra en la cabecera.
    /// - Que todos los métodos que se muestren en Swagger, muestren los comentarios puestos en los métodos de las funciones.
    /// </summary>
    /// <see href="https://learn.microsoft.com/es-es/aspnet/core/tutorials/getting-started-with-swashbuckle"/>
    public static void AddCustomSwagger(this IServiceCollection services)
    {
        services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc("v1", new OpenApiInfo
            {
                Version = "v1",
                Title = WebApiResources.WebApi_Titulo,
                Description = WebApiResources.WebApi_Descripcion,
                TermsOfService = new Uri(WebApiResources.WebApi_TerminosLicenciaUrl),
                Contact = new OpenApiContact
                {
                    Name = WebApiResources.WebApi_Contacto,
                    Url = new Uri(WebApiResources.WebApi_ContactoUrl)
                },
                License = new OpenApiLicense
                {
                    Name = WebApiResources.WebApi_Licencia,
                    Url = new Uri(WebApiResources.WebApi_LicenciaUrl)
                }
            });

            // Recoge todos los comentarios de los métodos para enriquecer el Web API
            var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
        });
    }
}