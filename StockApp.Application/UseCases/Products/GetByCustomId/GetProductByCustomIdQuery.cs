using StockApp.Application.DTOs.Responses.Products;
using StockApp.Application.UseCases.Categories;
using StockApp.Shared;

namespace StockApp.Application.UseCases.Products.GetByCustomId;

public sealed record GetProductByCustomIdQuery(string UserId, string ProductId) 
    : CommandQueryBase<Result<ProductDto>>(UserId);