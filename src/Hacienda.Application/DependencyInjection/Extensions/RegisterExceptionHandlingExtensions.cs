using Hacienda.Application.Exceptions.Sanitize;
using Hacienda.Application.ProblemDetails;
using Hacienda.Shared.Core.Exceptions.Middlewares;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Hacienda.Application.DependencyInjection.Extensions;

public static class RegisterExceptionHandlingExtensions
{
    /// <summary>
    /// Registra en el contenedor de DI los middlewares de excepciones personalizadas
    /// </summary>
    /// <param name="app">Application Builder</param>
    /// <returns>Application Builder configurada</returns>
    public static IApplicationBuilder UseExceptionHandling(
        this IApplicationBuilder app)
    {
        app.UseMiddleware<ExceptionHandlingMiddleware>();
        return app;
    }

    /// <summary>
    /// Registra en el contenedor de DI las políticas de saneamiento de excepciones y los problem details
    /// </summary>
    /// <param name="services">Service collection</param>
    /// <returns>Colección configurada</returns>
    public static IServiceCollection RegisterExceptionAndProblemDetails(
        this IServiceCollection services)
    {
        //Debe ser singleton porque se utiliza en un middlewares, el cual por defecto es singleton.
        services.AddSingleton<IExceptionPolicy, SanitizeNotControlledExceptionsPolicy>();
        services.AddSingleton<IProblemDetailsFactory, ProblemDetailsFactory>();

        return services;
    }
}