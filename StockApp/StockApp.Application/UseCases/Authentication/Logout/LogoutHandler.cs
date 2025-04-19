using MediatR;
using StockApp.Domain.Abstractions;
using StockApp.Shared;

namespace StockApp.Application.UseCases.Authentication.Logout;

public class LogoutHandler : IRequestHandler<LogoutCommand, Result>
{
    public Task<Result> Handle(LogoutCommand request, CancellationToken cancellationToken)
    {
        // Não faz nada, criado somente para seguir o padrão CQRS.
        return Task.FromResult(Result.Success());
    }
}