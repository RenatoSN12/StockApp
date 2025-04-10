using System.Net.Http.Json;
using StockApp.Domain.Abstractions;
using StockApp.Domain.Abstractions.Results;
using StockApp.Domain.DTOs.Requests.Categories;
using StockApp.Domain.DTOs.Responses;
using StockApp.Web.Services.Abstractions;

namespace StockApp.Web.Services.Implementations;

public class CategoryService(IHttpClientFactory httpClientFactory) : ICategoryService
{
    private readonly HttpClient _httpClient = httpClientFactory.CreateClient(Configuration.HttpClientName);

    public async Task<Result<PagedResponse<List<CategoryDto>>>> GetAllCategoriesAsync(
        CancellationToken cancellationToken = default)
    {
        var response = await _httpClient.GetAsync("api/categories", cancellationToken);

        if (!response.IsSuccessStatusCode)
            return await ErrorManager.CreateTypedFailureFromResponse<PagedResponse<List<CategoryDto>>>(response);

        var data = await response.Content
            .ReadFromJsonAsync<PagedResponse<List<CategoryDto>>>(cancellationToken: cancellationToken);

        return Result<PagedResponse<List<CategoryDto>>>.Success(data!);
    }

    public async Task<Result<CategoryDto?>> CreateCategoryAsync(CreateCategoryDto request,
        CancellationToken cancellationToken = default)
    {
        var result = await _httpClient.PostAsJsonAsync("/api/categories", request, cancellationToken);

        if (!result.IsSuccessStatusCode)
            return await ErrorManager.CreateTypedFailureFromResponse<CategoryDto?>(result);
        
        var readResult = await result.Content.ReadFromJsonAsync<CategoryDto?>(cancellationToken);

        return Result<CategoryDto?>.Success(readResult);
    }

    public async Task<Result> DeleteAsync(long id, CancellationToken cancellationToken = default)
    {
        var result = await _httpClient.DeleteAsync($"/api/categories/{id}", cancellationToken);

        if (result.IsSuccessStatusCode)
            return Result.Success("Categoria excluída com sucesso.");

        return await ErrorManager.CreateFailureFromResponse(result);
    }

    public async Task<Result<CategoryDto?>> UpdateAsync(long id, UpdateCategoryDto request, CancellationToken cancellationToken = default)
    {
        var result = await _httpClient.PutAsJsonAsync($"/api/categories/{id}", request, cancellationToken);
        
        if (!result.IsSuccessStatusCode)
            return await ErrorManager.CreateTypedFailureFromResponse<CategoryDto?>(result);
        
        var readResult = await result.Content.ReadFromJsonAsync<CategoryDto?>(cancellationToken);
        
        return Result<CategoryDto?>.Success(readResult);
    }
}