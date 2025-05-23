using StockApp.Application.DTOs.Requests.Categories;
using StockApp.Application.DTOs.Responses.Categories;
using StockApp.Shared;

namespace StockApp.Application.UseCases.Categories.Update;

public sealed record UpdateCategoryQuery(UpdateCategoryDto Dto, int Id, string UserId)
    : CommandQueryBase<Result<CategoryDto>>(UserId);