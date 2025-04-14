using MediatR;
using StockApp.Application.DTOs.Responses;
using StockApp.Application.DTOs.Responses.Categories;
using StockApp.Domain.DTOs.Responses;
using StockApp.Shared;

namespace StockApp.Application.UseCases.Categories.GetAll;

public sealed record GetAllCategoriesQuery(string UserId, int PageNumber, int PageSize)
        : CommandBase<Result<PagedResponse<List<CategoryDto>?>>>(UserId);
