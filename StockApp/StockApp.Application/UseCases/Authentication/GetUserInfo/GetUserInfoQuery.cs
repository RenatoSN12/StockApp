using StockApp.Application.UseCases.Categories;
using StockApp.Domain.Abstractions.Results;
using StockApp.Domain.DTOs.Responses;

namespace StockApp.Application.UseCases.Authentication.GetUserInfo;

public sealed record GetUserInfoQuery(string? UserId) : CommandBase<Result<UserDto?>>(UserId!); 