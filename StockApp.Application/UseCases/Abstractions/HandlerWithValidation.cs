using FluentValidation;
using StockApp.Shared;

namespace StockApp.Application.UseCases.Abstractions;

public abstract class HandlerWithValidation<TRequest>(IValidator<TRequest> validator) : IValidatableHandler<TRequest>
{
    public virtual async Task<Result> ValidateRequestAsync(TRequest request, CancellationToken cancellationToken = default)
    {
        var result = await validator.ValidateAsync(request, cancellationToken);

        return !result.IsValid
            ? Result.Failure(new Error("400", string.Join("\n", result.Errors.Select(e => e.ErrorMessage))))
            : Result.Success();
    }
}