using System.Net.Http.Json;
using StockApp.Application.DTOs.Requests.Location;
using StockApp.Application.DTOs.Responses.Location;
using StockApp.Shared;
using StockApp.Web.Services.Abstractions;

namespace StockApp.Web.Services.Implementations;

public class LocationService(IHttpClientFactory httpClientFactory) : ILocationService
{
    private readonly HttpClient _httpClient = httpClientFactory.CreateClient(Configuration.HttpClientName);
    
    public async Task<Result<PagedResponse<List<LocationDto>>>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        var response = await _httpClient.GetAsync("api/locations", cancellationToken);

        if (!response.IsSuccessStatusCode)
            return await ErrorManager.CreateTypedFailureFromResponse<PagedResponse<List<LocationDto>>>(response);

        var data = await response.Content
            .ReadFromJsonAsync<PagedResponse<List<LocationDto>>>(cancellationToken: cancellationToken);

        return Result<PagedResponse<List<LocationDto>>>.Success(data!);
    }

    public async Task<Result<LocationDto>> CreateAsync(CreateLocationDto request, CancellationToken cancellationToken = default)
    {
        var response = await _httpClient.PostAsJsonAsync("api/locations", request, cancellationToken);
        
        if (!response.IsSuccessStatusCode)
            return await ErrorManager.CreateTypedFailureFromResponse<LocationDto>(response);

        var data = await response.Content
            .ReadFromJsonAsync<LocationDto>(cancellationToken: cancellationToken);

        return Result<LocationDto>.Success(data!);
    }
}