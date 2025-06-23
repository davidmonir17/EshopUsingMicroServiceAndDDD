﻿using BuldingBlocks.CQRS;
using FluentValidation;
using MediatR;

namespace BuldingBlocks.Behaviors
{
    public class ValidationBehavior<TRequest, TResponse>(IEnumerable<IValidator<TRequest>> validators)
        : IPipelineBehavior<TRequest, TResponse>
        where TRequest : ICommand<TResponse>
    {
        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            var context = new ValidationContext<TRequest>(request);
            var validationResult = await Task.WhenAll(
                validators.Select(v => v.ValidateAsync(context, cancellationToken))
            );
            var failures = validationResult
                .Where(failure => failure.Errors.Any())
                .SelectMany(result => result.Errors)
                .ToList();
            if (failures.Any())
                throw new ValidationException(failures);
            return await next();
        }
    }
}
