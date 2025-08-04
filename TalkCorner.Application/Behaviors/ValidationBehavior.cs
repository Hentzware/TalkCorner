using FluentValidation;
using FluentValidation.Results;
using MediatR;
using TalkCorner.Application.Exceptions;

namespace TalkCorner.Application.Behaviors;

public class ValidationBehavior<TRequest, TResponse>(IEnumerable<IValidator<TRequest>> validators) : IPipelineBehavior<TRequest, TResponse> where TRequest : notnull
{
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        if (validators.Any())
        {
            var context = new ValidationContext<TRequest>(request);
            var validationResults = await Task.WhenAll(validators.Select(x => x.ValidateAsync(context, cancellationToken)));
            var allFailures = validationResults
                .SelectMany(r => r.Errors)
                .Where(f => f != null)
                .ToList();

            if (allFailures.Count > 0)
            {
                var aggregateResult = new ValidationResult(allFailures);

                throw new BadRequestException("One or more validation errors occurred.", aggregateResult);
            }
        }

        return await next(cancellationToken);
    }
}