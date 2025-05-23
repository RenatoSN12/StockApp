using System.Net.Http.Json;
using StockApp.Application.DTOs.Requests.ProductStock;
using StockApp.Application.DTOs.Responses.ProductStock;
using StockApp.Shared;
using StockApp.Web.Services.Abstractions;

namespace StockApp.Web.Services.Implementations;

public class ProductStockService(IHttpClientFactory httpClientFactory) 
    : ServiceBase, IProductStockService
{
    private readonly HttpClient _httpClient = httpClientFactory.CreateClient(Configuration.HttpClientName);
    
    public async Task<Result<ProductStockDto>> AddAsync(CreateProductStockDto request, CancellationToken cancellationToken = default)
    {
        return await SendRequestAsync<ProductStockDto>(
            () => _httpClient.PostAsJsonAsync("api/product-stock", request, cancellationToken), 
            cancellationToken);
    }

    public async Task<Result<string>> DeleteAsync(DeleteProductStockDto request, CancellationToken cancellationToken = default)
    {
        var requestMessage = new HttpRequestMessage
        {
            Method = HttpMethod.Delete,
            RequestUri = new Uri("/api/product-stock", UriKind.Relative),
            Content = JsonContent.Create(request)
        };
        
        var response = await _httpClient.SendAsync(requestMessage, cancellationToken);
        if (!response.IsSuccessStatusCode)
            return await ErrorManager.CreateTypedFailureFromResponse<string>(response);       
        
        var data = await response.Content.ReadAsStringAsync(cancellationToken);
        return Result.Success(data);
    }
}