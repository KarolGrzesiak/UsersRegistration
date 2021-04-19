using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;
using ILogger = DnsClient.Internal.ILogger;

namespace Infrastructure.Decorators
{
    public class LoggingCommandQueryHandlerDecorator<TRequest,TResponse> : IRequestHandler<TRequest, TResponse> where TRequest : IRequest<TResponse>
    {
        private readonly IRequestHandler<TRequest, TResponse> _handler;
        private readonly ILogger<TRequest> _logger;

        public LoggingCommandQueryHandlerDecorator(IRequestHandler<TRequest,TResponse> handler, ILogger<TRequest> logger)
        {
            _handler = handler;
            _logger = logger;
        }

        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation("Starting processing request: {@Request}", typeof(TRequest).Name);
                var result = await _handler.Handle(request, cancellationToken);
                _logger.LogInformation("Finished processing request: {@Request}", typeof(TRequest).Name);
                return result;
            }
            catch (Exception exception)
            {
                _logger.LogError("Error happened when processing request: {@Request}. Type of error: {@Exception}", typeof(TRequest).Name, exception.GetType().Name);
                throw;
            }
        }
    }
}