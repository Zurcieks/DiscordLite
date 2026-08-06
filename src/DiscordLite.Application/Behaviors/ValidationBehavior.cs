using DiscordLite.Application.Exceptions;
using FluentValidation;
using MediatR;

namespace DiscordLite.Application.Behaviors;

public sealed class ValidationBehavior<TRequest, TResponse>(
    IEnumerable<IValidator<TRequest>> validators)
    : IPipelineBehavior<TRequest, TResponse>
    where TRequest : notnull
{
    public async Task<TResponse> Handle(
        TRequest request,
        RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        var validatorArray = validators.ToArray();

        if (validatorArray.Length == 0)
            return await next(cancellationToken);

        var context = new ValidationContext<TRequest>(request);

        var validationResults = await Task.WhenAll(
            validatorArray.Select(validator =>
                validator.ValidateAsync(context, cancellationToken)));

        var errors = validationResults
            .SelectMany(result => result.Errors)
            .Where(failure => failure is not null)
            .GroupBy(failure => failure.PropertyName)
            .ToDictionary(
                group => group.Key,
                group => group.Select(failure => failure.ErrorMessage).Distinct().ToArray());

        if (errors.Count > 0)
            throw new ValidationRequestException(errors);

        return await next(cancellationToken);
    }
}