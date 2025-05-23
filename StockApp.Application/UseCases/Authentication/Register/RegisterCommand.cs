using MediatR;
using StockApp.Application.DTOs.Responses.Authentication;
using StockApp.Shared;

namespace StockApp.Application.UseCases.Authentication.Register;

public sealed record RegisterCommand(string FirstName, string LastName,string Email, string Password) : IRequest<Result<UserDto>>;
