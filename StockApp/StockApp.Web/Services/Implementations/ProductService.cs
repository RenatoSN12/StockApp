using System.Net.Http.Json;
using StockApp.Application.DTOs.Requests.Products;
using StockApp.Application.DTOs.Responses.Products;
using StockApp.Shared;
using StockApp.Web.Services.Abstractions;

namespace StockApp.Web.Services.Implementations;

public class ProductService(IHttpClientFactory httpClientFactory) : IProductService
{
    private readonly HttpClient _httpClient = httpClientFactory.CreateClient(Configuration.HttpClientName);
    
    public async Task<Result<PagedResponse<List<ResumeProductDto>>>> GetAllAsync(CancellationToken cancellationToken)
    {
        var response = await _httpClient.GetAsync($"/api/products?pageNumber=1&pageSize=25", cancellationToken);

        if (!response.IsSuccessStatusCode)
            return await ErrorManager.CreateTypedFailureFromResponse<PagedResponse<List<ResumeProductDto>>>(response);

        var data = await response.Content
            .ReadFromJsonAsync<PagedResponse<List<ResumeProductDto>>>(cancellationToken);

        return Result<PagedResponse<List<ResumeProductDto>>>.Success(data!);
    }

    public async Task<Result<ProductDto>> CreateAsync(CreateProductDto createProductDto,
        CancellationToken cancellationToken)
    {
        var response = await _httpClient.PostAsJsonAsync($"/api/products", createProductDto, cancellationToken);
        
        if(!response.IsSuccessStatusCode)
            return await ErrorManager.CreateTypedFailureFromResponse<ProductDto>(response);
        
        var data = await response.Content.ReadFromJsonAsync<ProductDto>(cancellationToken);
        
        return Result<ProductDto>.Success(data!);
    }
}