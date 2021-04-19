using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using MediatR;

namespace Application.Shared.Decorators
{
    public class ValidationCommandQueryHandlerDecorator<TRequest,TResponse> : IRequestHandler<TRequest, TResponse> where TRequest : IRequest<TResponse>
    {
        private readonly IRequestHandler<TRequest,TResponse> _handler;
        private readonly IEnumerable<IValidator<TRequest>> _validators;

        public ValidationCommandQueryHandlerDecorator(IRequestHandler<TRequest,TResponse> handler, IEnumerable<IValidator<TRequest>> validators)
        {
            _handler = handler;
            _validators = validators;
        }
        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken)
        {
            if (_validators.Any())
            {
                var context = new ValidationContext<TRequest>(request);

                var validationResults = await Task.WhenAll(_validators.Select(v => v.ValidateAsync(context, cancellationToken)));
                var failures = validationResults.SelectMany(r => r.Errors).Where(f => f != null).ToList();

                if (failures.Count != 0)
                    throw new ValidationException(failures);
            }
            return await _handler.Handle(request, cancellationToken);
        }
    }
}