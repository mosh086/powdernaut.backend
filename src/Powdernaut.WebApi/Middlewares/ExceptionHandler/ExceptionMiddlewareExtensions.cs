using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.WebUtilities;
using Powdernaut.Application.Exceptions;
using Powdernaut.Domain.Exceptions;
using Powdernaut.Infrastructure.Exceptions;
using System.Text.Json;

namespace Powdernaut.WebApi.Middlewares.ExceptionHandler;

public static class ExceptionMiddlewareExtensions
{
    public static IApplicationBuilder UseExceptionMiddleware(this IApplicationBuilder builder)
    {
        return builder.UseExceptionHandler(exceptionHandlerApp =>
        {
            exceptionHandlerApp.Run(async context =>
            {
                var logger = context.RequestServices
                    .GetRequiredService<ILoggerFactory>()
                    .CreateLogger(nameof(ExceptionMiddlewareExtensions));
                var exceptionHandlerPathFeature = context.Features.Get<IExceptionHandlerPathFeature>();
                var exception = exceptionHandlerPathFeature?.Error;

                var statusCode = exception switch
                {
                    null => StatusCodes.Status500InternalServerError,
                    UnauthorizedAccessException => StatusCodes.Status403Forbidden,
                    IdentityException => StatusCodes.Status400BadRequest,
                    DomainException => StatusCodes.Status400BadRequest,
                    NotFoundException => StatusCodes.Status404NotFound,
                    _ => StatusCodes.Status500InternalServerError
                };

                context.Response.StatusCode = statusCode;
                context.Response.ContentType = "application/problem+json";

                if (exception is null)
                {
                    logger.LogError("An unknown error occurred while handling the request.");
                    await WriteProblemDetailsAsync(context, statusCode, "An unexpected error occurred.");
                    return;
                }

                var routeData = context.GetRouteData();
                var controller = routeData?.Values["controller"]?.ToString() ?? context.Request.Path;
                var method = routeData?.Values["action"]?.ToString() ?? context.Request.Method;

                logger.LogError(exception,
                    "Unhandled exception occurred in {Controller}.{Method}",
                    controller,
                    method);

                var publicMessage = exception switch
                {
                    NotFoundException => "The requested resource was not found.",
                    DomainException => "The request could not be processed.",
                    IdentityException => "The identity operation could not be completed.",
                    UnauthorizedAccessException => "You are not authorized to perform this operation.",
                    _ => "An unexpected error occurred."
                };

                await WriteProblemDetailsAsync(context, statusCode, publicMessage);
            });
        });
    }

    private static Task WriteProblemDetailsAsync(HttpContext context, int statusCode, string detail)
    {
        var problemDetails = new Microsoft.AspNetCore.Mvc.ProblemDetails
        {
            Status = statusCode,
            Title = ReasonPhrases.GetReasonPhrase(statusCode),
            Detail = detail,
            Instance = context.Request.Path
        };
        problemDetails.Extensions["traceId"] = context.TraceIdentifier;

        return context.Response.WriteAsync(JsonSerializer.Serialize(problemDetails, JsonSerializerOptions.Web));
    }
}
