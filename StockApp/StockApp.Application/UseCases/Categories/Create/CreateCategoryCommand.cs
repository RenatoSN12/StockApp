using StockApp.Domain.Abstractions.Results;
using StockApp.Domain.DTOs.Requests.Categories;
using StockApp.Domain.DTOs.Responses;

namespace StockApp.Application.UseCases.Categories.Create;

public sealed record CreateCategoryCommand(string UserId, CreateCategoryDto CreateCategoryDto)
    : CommandBase<Result<CategoryDto>>(UserId);
