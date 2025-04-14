using StockApp.Application.DTOs.Requests.Products;
using StockApp.Application.DTOs.Responses.Products;
using StockApp.Application.UseCases.Categories;
using StockApp.Shared;

namespace StockApp.Application.UseCases.Products.Create;

public sealed record CreateProductCommand(CreateProductDto Dto, string UserId) : CommandBase<Result<ProductDto>>(UserId);
