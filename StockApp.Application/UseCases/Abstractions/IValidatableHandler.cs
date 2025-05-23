using StockApp.Shared;

namespace StockApp.Application.UseCases.Abstractions;

public interface IValidatableHandler<TRequest>
{
    Task<Result> ValidateRequestAsync(TRequest request, CancellationToken cancellationToken);
}