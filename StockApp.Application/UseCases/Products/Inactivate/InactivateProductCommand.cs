using StockApp.Application.DTOs.Responses.Products;
using StockApp.Application.UseCases.Categories;
using StockApp.Shared;

namespace StockApp.Application.UseCases.Products.Inactivate;

public sealed record InactivateProductCommand(string UserId, long ProductId) : CommandQueryBase<Result<ProductDto>>(UserId);