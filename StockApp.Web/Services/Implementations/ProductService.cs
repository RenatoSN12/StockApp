using System.Net.Http.Json;
using StockApp.Application.DTOs.Requests.Products;
using StockApp.Application.DTOs.Responses.Products;
using StockApp.Shared;
using StockApp.Web.Services.Abstractions;

namespace StockApp.Web.Services.Implementations;

public class ProductService(IHttpClientFactory httpClientFactory) : ServiceBase, IProductService
{
    private readonly HttpClient _httpClient = httpClientFactory.CreateClient(Configuration.HttpClientName);

    public async Task<Result<PagedResponse<List<ResumeProductDto>>>> GetAllAsync(CancellationToken cancellationToken)
        => await SendRequestAsync<PagedResponse<List<ResumeProductDto>>>(() =>
            _httpClient.GetAsync("api/products?pageNumber=1&pageSize=25", cancellationToken), cancellationToken);

    public async Task<Result<ProductDto>> GetByCustomId(string customId, CancellationToken cancellationToken = default)
        => await SendRequestAsync<ProductDto>(
            () => _httpClient.GetAsync($"/api/products/{customId}", cancellationToken), cancellationToken);
    
    public async Task<Result<ProductDto>> CreateAsync(CreateProductDto createProductDto,
        CancellationToken cancellationToken)
        => await SendRequestAsync<ProductDto>(
            () => _httpClient.PostAsJsonAsync($"/api/products", createProductDto, cancellationToken),
            cancellationToken);

    public async Task<Result<ProductDto>> UpdateAsync(ProductDto productDto,
        CancellationToken cancellationToken = default)
        => await SendRequestAsync<ProductDto>(
            () => _httpClient.PutAsJsonAsync($"/api/products/", productDto, cancellationToken),
            cancellationToken);

    public async Task<Result<ProductDto>> InactivateAsync(long productId, CancellationToken cancellationToken = default)
        => await SendRequestAsync<ProductDto>(() => _httpClient.PutAsJsonAsync($"/api/products/{productId}/inactivate",
            productId, cancellationToken), cancellationToken);

    public async Task<Result<ProductDto>> ActivateAsync(long productId, CancellationToken cancellationToken = default)
        => await SendRequestAsync<ProductDto>(() => _httpClient.PutAsJsonAsync($"/api/products/{productId}/activate",
            productId, cancellationToken), cancellationToken);
}