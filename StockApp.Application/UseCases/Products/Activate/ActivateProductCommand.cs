using StockApp.Application.DTOs.Responses.Products;
using StockApp.Application.UseCases.Categories;
using StockApp.Shared;

namespace StockApp.Application.UseCases.Products.Activate;

public sealed record ActivateProductCommand(string UserId, long ProductId) : CommandQueryBase<Result<ProductDto>>(UserId);