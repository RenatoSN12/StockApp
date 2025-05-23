using StockApp.Application.DTOs.Responses.Categories;
using StockApp.Shared;

namespace StockApp.Application.UseCases.Categories.GetAll;

public sealed record GetAllCategoriesQuery(string UserId, int PageNumber, int PageSize)
        : CommandQueryBase<Result<PagedResponse<List<CategoryDto>>>>(UserId);
