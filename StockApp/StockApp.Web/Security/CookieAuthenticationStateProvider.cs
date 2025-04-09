using System.Net.Http.Json;
using System.Security.Claims;
using Microsoft.AspNetCore.Components.Authorization;
using StockApp.Domain.Abstractions;
using StockApp.Domain.Abstractions.Results;
using StockApp.Domain.DTOs.Responses;

namespace StockApp.Web.Security;

public class CookieAuthenticationStateProvider(IHttpClientFactory clientFactory)
    : AuthenticationStateProvider, ICookieAuthenticationStateProvider
{
    private readonly HttpClient _client = clientFactory.CreateClient(Configuration.HttpClientName);
    private bool _isAuthenticated = false;

    public async Task<bool> CheckAuthenticatedAsync()
    {
        await GetAuthenticationStateAsync();
        return _isAuthenticated;
    }

    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        _isAuthenticated = false;
        try
        {
            var result = await _client.GetAsync("api/user/info");

            if (!result.IsSuccessStatusCode)
                return SetUnauthenticated();

            var user = await result.Content.ReadFromJsonAsync<UserDto>();

            if (user is null)
                return SetUnauthenticated();

            var claims = new Claim[]
            {
                new(ClaimTypes.Name, $"{user.Firstname} {user.Lastname}"),
                new(ClaimTypes.Email, user.Email)
            };

            var identity = new ClaimsIdentity(claims, "Cookies");
            _isAuthenticated = true;
            return new AuthenticationState(new ClaimsPrincipal(identity));
        }
        catch
        {
            return SetUnauthenticated();
        }
    }

    private AuthenticationState SetUnauthenticated()
    {
        _isAuthenticated = false;
        return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));
    }
    public void NotifyAuthenticationStateChanged() =>
        base.NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
}