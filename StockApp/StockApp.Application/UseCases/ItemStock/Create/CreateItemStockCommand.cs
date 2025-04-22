using StockApp.Application.DTOs.Requests.ItemStock;
using StockApp.Application.DTOs.Responses.ItemStock;
using StockApp.Application.UseCases.Categories;
using StockApp.Shared;

namespace StockApp.Application.UseCases.ItemStock.Create;

public sealed record CreateItemStockCommand(string UserId, CreateItemStockDto Dto) : CommandQueryBase<Result<ItemStockDto>>(UserId);