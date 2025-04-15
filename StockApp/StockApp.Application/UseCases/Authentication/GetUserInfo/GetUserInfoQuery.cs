using StockApp.Application.DTOs.Responses.Authentication;
using StockApp.Application.UseCases.Categories;
using StockApp.Shared;

namespace StockApp.Application.UseCases.Authentication.GetUserInfo;

public sealed record GetUserInfoQuery(string? UserId) : CommandQueryBase<Result<UserDto?>>(UserId!); 