using MediatR;
using Microsoft.Extensions.Logging;
using System.Diagnostics;

namespace ProductStore.Api.Domain.Behaviors
{
    public class LoggingBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
    {
        private readonly ILogger<LoggingBehavior<TRequest, TResponse>> _logger;
        public LoggingBehavior(ILogger<LoggingBehavior<TRequest, TResponse>> logger) => _logger = logger;

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            _logger.LogInformation($"STARTING Request for: {typeof(TRequest).Name}");
            
            var response = await next();

            stopwatch.Stop();
            _logger.LogInformation($"FINISH Request for: {typeof(TRequest).Name} with a duration: {stopwatch.Elapsed.TotalMilliseconds} ms");

            return response;
        }
    }
}
