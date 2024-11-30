using System.Diagnostics;
using MediatR;
using Microsoft.Extensions.Logging;

namespace BuildingBlocks.Behaviors;

public class LoggingBehavior<TRequest, TResponse>(ILogger<LoggingBehavior<TRequest, TResponse>> logger) 
    : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
    where TResponse : notnull
{
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        logger.LogInformation("[START] Handle request={Request} - Response={Response} - RequestData={RequestData}", 
            typeof(TRequest).Name, typeof(TResponse).Name, request);

        var timer = Stopwatch.StartNew();
        var response = await next();
        timer.Stop();
        
        var timeTaken = timer.Elapsed;
        if(timeTaken.Seconds > 3) //if the response is greater than 3 seconds, then log the warnings
            logger.LogWarning("[PERFORMANCE] The request {Request} is being processed in {TimeTaken} seconds.",
                typeof(TRequest).Name, timeTaken.Seconds);
        logger.LogInformation("[END] Handle request={Request} with Response={Response}", typeof(TRequest).Name, typeof(TResponse).Name);
        return response;
        
    }
}