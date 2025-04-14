using StockApp.Application.DTOs.Responses.Authentication;
using StockApp.Application.UseCases.Categories;
using StockApp.Domain.DTOs.Responses;
using StockApp.Shared;

namespace StockApp.Application.UseCases.Authentication.GetUserInfo;

public sealed record GetUserInfoQuery(string? UserId) : CommandBase<Result<UserDto?>>(UserId!); 