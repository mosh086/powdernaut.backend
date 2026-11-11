using Powdernaut.Infrastructure.Exceptions;
using Microsoft.AspNetCore.Diagnostics;
using Newtonsoft.Json;
using System.Net.Mime;

namespace Powdernaut.WebApi.Middlewares.ExceptionHandler;

public class ApiError
{
    public string Message { get; set; } = string.Empty;
    public string Controller { get; set; } = string.Empty;
    public string Method { get; set; } = string.Empty;
    public string Error { get; set; } = string.Empty;
    public string InnerError { get; set; } = string.Empty;
    public string StackTrace { get; set; } = string.Empty;
}

public static class ExceptionMiddlewareExtensions
{
    public static IApplicationBuilder UseExceptionMiddleware(this IApplicationBuilder builder)
    {
        return builder.UseExceptionHandler(exceptionHandlerApp =>
        {
            exceptionHandlerApp.Run(async context =>
            {
                string getMessage(Exception exception)
                {
                    if (exception is IdentityException)
                    {
                        return string.Join("|", (exception as IdentityException)!.Errors);
                    }
                    return exception.Message;
                }

                var logger = context.RequestServices.GetRequiredService<ILogger<ApiError>>();
                var exceptionHandlerPathFeature = context.Features.Get<IExceptionHandlerPathFeature>();
                var exception = exceptionHandlerPathFeature?.Error;

                context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                context.Response.ContentType = MediaTypeNames.Application.Json;

                if (exception != null)
                {
                    var routeData = context.GetRouteData();

                    var error = new ApiError
                    {
                        Message = getMessage(exception),
                        Controller = routeData?.Values["controller"]?.ToString() ?? context.Request.Path,
                        Method = routeData?.Values["action"]?.ToString() ?? context.Request.Method,
                        Error = exception.GetType().Name,
                        //StackTrace = exception.StackTrace
                    };


                    // Build inner exception chain
                    var innerException = exception.InnerException;
                    if (innerException != null)
                    {
                        var innerErrors = new List<string>();
                        while (innerException != null)
                        {
                            innerErrors.Add($"{innerException.GetType().Name}: {innerException.Message}");
                            innerException = innerException.InnerException;
                        }
                        error.InnerError = string.Join(" => ", innerErrors);
                    }

                    // Log the error
                    logger.LogError(exception,
                        "Unhandled exception occurred in {Controller}.{Method}: {ErrorMessage}",
                        error.Controller,
                        error.Method,
                        error.Message);

                    await context.Response.WriteAsync(JsonConvert.SerializeObject(error));
                }
                else
                {
                    logger.LogError("An unknown error occurred");
                    await context.Response.WriteAsync(JsonConvert.SerializeObject(new ApiError
                    {
                        Message = "An unknown error occurred"
                    }));
                }
            });
        });
    }
}
