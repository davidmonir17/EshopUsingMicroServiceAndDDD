using MediatR;
using Microsoft.Extensions.Logging;
using System.Diagnostics;

namespace BuldingBlocks.Behaviors
{
    public class LoggingBehavior<TRequest, TResponse>(ILogger<LoggingBehavior<TRequest, TResponse>> logger)
        : IPipelineBehavior<TRequest, TResponse>
        where TRequest : notnull, IRequest<TResponse>
        where TResponse : notnull
    {
        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            logger.LogInformation("[START] handle request={Request} - Response={Response}- RequestData={RequestData}", typeof(TRequest).Name,
                typeof(TResponse).Name, request);

            var timer = new Stopwatch();
            timer.Start();
            var response = await next();
            timer.Stop();
            var elapsedTime = timer.Elapsed;
            if (elapsedTime.Seconds > 3)
            {
                logger.LogWarning("[Performance] the request {Request} took {ElapsedTime} seconds to complete.",
                    typeof(TRequest).Name, elapsedTime.TotalSeconds);
            }
            logger.LogInformation("[END] handle request={Request} - Response={Response} - ElapsedTime={ElapsedTime} seconds",
                typeof(TRequest).Name, typeof(TResponse).Name, elapsedTime.TotalSeconds);

            return response;

        }
    }
}
