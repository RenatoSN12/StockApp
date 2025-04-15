using StockApp.Application.DTOs.Requests.Categories;
using StockApp.Application.DTOs.Responses.Categories;
using StockApp.Shared;

namespace StockApp.Application.UseCases.Categories.Create;

public sealed record CreateCategoryCommandQuery(string UserId, CreateCategoryDto Dto)
    : CommandQueryBase<Result<CategoryDto>>(UserId);
