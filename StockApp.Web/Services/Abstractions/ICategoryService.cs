using StockApp.Application.DTOs.Requests.Categories;
using StockApp.Application.DTOs.Responses.Categories;
using StockApp.Shared;

namespace StockApp.Web.Services.Abstractions;

public interface ICategoryService
{
    Task<Result<PagedResponse<List<CategoryDto>>>> GetAllCategoriesAsync(CancellationToken cancellationToken = default);
    Task<Result<CategoryDto?>> CreateAsync(CreateCategoryDto request, CancellationToken cancellationToken = default);
    Task<Result> DeleteAsync(long id, CancellationToken cancellationToken = default);
    Task<Result<CategoryDto?>> UpdateAsync(long id, UpdateCategoryDto request, CancellationToken cancellationToken = default);
}