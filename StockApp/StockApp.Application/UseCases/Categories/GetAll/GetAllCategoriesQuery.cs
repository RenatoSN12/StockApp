using MediatR;
using StockApp.Domain.Abstractions.Results;
using StockApp.Domain.DTOs.Responses;

namespace StockApp.Application.UseCases.Categories.GetAll;

public sealed record GetAllCategoriesQuery(string UserId, int PageNumber, int PageSize)
    : IRequest<PagedResult<List<CategoryDto>?>>;
