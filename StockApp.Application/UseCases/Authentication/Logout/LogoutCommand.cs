using MediatR;
using StockApp.Domain.Abstractions;
using StockApp.Shared;

namespace StockApp.Application.UseCases.Authentication.Logout;

public sealed record LogoutCommand : IRequest<Result>;