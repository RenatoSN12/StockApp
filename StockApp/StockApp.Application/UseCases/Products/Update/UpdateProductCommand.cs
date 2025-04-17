using StockApp.Application.DTOs.Responses.Products;
using StockApp.Application.UseCases.Categories;
using StockApp.Shared;

namespace StockApp.Application.UseCases.Products.Update;

public sealed record UpdateProductCommand(ProductDto Dto, string UserId) : CommandQueryBase<Result<ProductDto>>(UserId);