using MediatR;
using Microsoft.Extensions.Logging;

namespace FraudDetector.Application.Behaviours;

public class LoggingBehaviour<TRequest, TResponse> 
    : IPipelineBehavior<TRequest, TResponse> where TRequest : notnull
{
    private readonly ILogger<TRequest> _logger;

    public LoggingBehaviour(ILogger<TRequest> logger)
    {
        _logger = logger;
    }

    public async Task<TResponse> Handle(
        TRequest request, 
        CancellationToken cancellationToken, 
        RequestHandlerDelegate<TResponse> next)
    {
        try
        {
            return await next();
        }
        catch (Exception ex)
        {
            _logger.LogError(
                ex, 
                "FraudDetector Request: Unhandled Exception for Request {Name} {@Request}",
                typeof(TRequest).Name, 
                request);

            throw;
        }
    }
}