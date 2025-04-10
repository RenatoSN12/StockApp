using MediatR;
using StockApp.Domain.Abstractions.Results;
using StockApp.Domain.DTOs.Requests.Categories;
using StockApp.Domain.DTOs.Responses;

namespace StockApp.Application.UseCases.Categories.Update;

public sealed record UpdateCategoryCommand(UpdateCategoryDto Dto, int Id, string UserId)
    : CommandBase<Result<CategoryDto>>(UserId);