using StockApp.Application.DTOs.Requests.Authentication;
using StockApp.Domain.Abstractions;
using StockApp.Shared;

namespace StockApp.Web.Services.Abstractions;

public interface IAuthService
{
    Task<Result> LoginAsync(LoginRequest request);
    Task LogoutAsync();
    Task<Result> RegisterAsync(RegisterRequest request);
}