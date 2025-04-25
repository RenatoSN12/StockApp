using StockApp.Application.DTOs.Requests.ProductStock;
using StockApp.Application.DTOs.Responses.ProductStock;
using StockApp.Application.UseCases.Categories;
using StockApp.Shared;

namespace StockApp.Application.UseCases.ProductStock.Create;

public sealed record CreateProductStockCommand(string UserId, CreateProductStockDto Dto) : CommandQueryBase<Result<ProductStockDto>>(UserId);