using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Hosting;
using Powdernaut.Application.Exceptions;
using Powdernaut.WebApi.Middlewares.ExceptionHandler;
using System.Net;
using System.Text.Json;

namespace Powdernaut.WebApi.Tests;

public class ExceptionMiddlewareTests
{
    [Fact]
    public async Task UseExceptionMiddleware_MapsNotFoundAndDoesNotExposeExceptionMessage()
    {
        using var host = await CreateServer(() => throw new NotFoundException("database table details"));
        using var client = host.GetTestClient();
        using var response = await client.GetAsync("/resource");
        var responseBody = await response.Content.ReadAsStringAsync();
        using var problem = JsonDocument.Parse(responseBody);

        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        Assert.Equal("application/problem+json", response.Content.Headers.ContentType?.MediaType);
        Assert.Equal((int)HttpStatusCode.NotFound, problem.RootElement.GetProperty("status").GetInt32());
        Assert.Equal("The requested resource was not found.", problem.RootElement.GetProperty("detail").GetString());
        Assert.True(problem.RootElement.TryGetProperty("traceId", out _));
        Assert.DoesNotContain("database table details", responseBody);
    }

    [Fact]
    public async Task UseExceptionMiddleware_ReturnsGenericProblemForUnexpectedException()
    {
        using var host = await CreateServer(() => throw new InvalidOperationException("private implementation details"));
        using var client = host.GetTestClient();
        using var response = await client.GetAsync("/resource");
        var responseBody = await response.Content.ReadAsStringAsync();
        using var problem = JsonDocument.Parse(responseBody);

        Assert.Equal(HttpStatusCode.InternalServerError, response.StatusCode);
        Assert.Equal((int)HttpStatusCode.InternalServerError, problem.RootElement.GetProperty("status").GetInt32());
        Assert.Equal("An unexpected error occurred.", problem.RootElement.GetProperty("detail").GetString());
        Assert.True(problem.RootElement.TryGetProperty("traceId", out _));
        Assert.DoesNotContain("private implementation details", responseBody);
    }

    private static async Task<IHost> CreateServer(Action throwException)
    {
        var host = new HostBuilder()
            .ConfigureWebHost(webHost => webHost
                .UseTestServer()
                .Configure(app =>
                {
                    app.UseExceptionMiddleware();

                    app.Run(context =>
                    {
                        throwException();
                        return Task.CompletedTask;
                    });
                }))
            .Build();

        await host.StartAsync();
        return host;
    }
}
