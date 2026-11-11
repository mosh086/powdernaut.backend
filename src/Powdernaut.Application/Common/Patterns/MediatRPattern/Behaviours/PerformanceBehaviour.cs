using Powdernaut.Application.Providers.UserManagement;
using Microsoft.Extensions.Logging;
using System.Diagnostics;

namespace Powdernaut.Application.Common.Patterns.MediatRPattern.Behaviours;

public class PerformanceBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : notnull
{
    private readonly ILogger<TRequest> _logger;
    private const long ThresholdMilliseconds = 500;
    private readonly IUser user;
    public PerformanceBehaviour(ILogger<TRequest> logger, IUser user)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        this.user = user;
    }

    public async Task<TResponse> Handle(
        TRequest request,
        RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        var stopwatch = Stopwatch.StartNew();

        try
        {
            var response = await next().ConfigureAwait(false);
            LogPerformanceIfSlow(stopwatch, request);
            return response;
        }
        catch (Exception ex) when (LogPerformanceOnException(stopwatch, request, ex))
        {
            throw;
        }
    }

    private void LogPerformanceIfSlow(Stopwatch stopwatch, TRequest request)
    {
        stopwatch.Stop();
        var elapsedMilliseconds = stopwatch.ElapsedMilliseconds;

        if (elapsedMilliseconds > ThresholdMilliseconds)
        {
            var requestName = typeof(TRequest).Name;

            _logger.LogWarning(
                "Powdernaut Long Running Request: {Name} ({ElapsedMilliseconds} milliseconds) {@UserId} {@UserName} {@Request}",
                requestName,
                elapsedMilliseconds,
                user.UserId,
                user.Username,
                request);
        }
    }

    private bool LogPerformanceOnException(Stopwatch stopwatch, TRequest request, Exception ex)
    {
        stopwatch.Stop();
        var elapsedMilliseconds = stopwatch.ElapsedMilliseconds;

        _logger.LogError(
            ex,
            "Request {Name} failed after {ElapsedMilliseconds} milliseconds: {@Request}",
            typeof(TRequest).Name,
            elapsedMilliseconds,
            request);

        return false;
    }
}