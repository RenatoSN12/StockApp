using MediatR;
using StockApp.Application.DTOs.Requests.Categories;
using StockApp.Application.DTOs.Responses.Categories;
using StockApp.Domain.DTOs.Responses;
using StockApp.Shared;

namespace StockApp.Application.UseCases.Categories.Update;

public sealed record UpdateCategoryCommand(UpdateCategoryDto Dto, int Id, string UserId)
    : CommandBase<Result<CategoryDto>>(UserId);