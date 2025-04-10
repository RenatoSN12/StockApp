using StockApp.Domain.Abstractions.Results;
using StockApp.Domain.DTOs.Requests.Categories;
using StockApp.Domain.DTOs.Responses;

namespace StockApp.Web.Services.Abstractions;

public interface ICategoryService
{
    Task<Result<PagedResponse<List<CategoryDto>>>> GetAllCategoriesAsync(CancellationToken cancellationToken = default);
    Task<Result<CategoryDto?>> CreateCategoryAsync(CreateCategoryDto request, CancellationToken cancellationToken = default);
    Task<Result> DeleteAsync(long id, CancellationToken cancellationToken = default);
    Task<Result<CategoryDto?>> UpdateAsync(long id, UpdateCategoryDto request, CancellationToken cancellationToken = default);
}