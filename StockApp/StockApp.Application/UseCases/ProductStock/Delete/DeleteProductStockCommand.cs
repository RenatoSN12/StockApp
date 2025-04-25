using StockApp.Application.DTOs.Requests.ProductStock;
using StockApp.Application.UseCases.Categories;
using StockApp.Shared;

namespace StockApp.Application.UseCases.ProductStock.Delete;

public sealed record DeleteProductStockCommand(string UserId, DeleteProductStockDto Dto) : CommandQueryBase<Result<string>>(UserId);