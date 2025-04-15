using StockApp.Application.DTOs.Responses.Products;
using StockApp.Application.UseCases.Categories;
using StockApp.Shared;

namespace StockApp.Application.UseCases.Products.GetAll;

public sealed record GetAllProductsQuery(string UserId, int PageNumber, int PageSize)
    : CommandQueryBase<Result<PagedResponse<List<ResumeProductDto>>>>(UserId);