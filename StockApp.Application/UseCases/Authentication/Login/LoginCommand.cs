using MediatR;
using StockApp.Application.DTOs.Responses.Authentication;
using StockApp.Shared;

namespace StockApp.Application.UseCases.Authentication.Login;

public sealed record LoginCommand(string Email, string Password) : IRequest<Result<UserDto>>;