using StockApp.Application.DTOs.Requests.ItemStock;
using StockApp.Application.UseCases.Categories;
using StockApp.Shared;

namespace StockApp.Application.UseCases.ItemStock.Delete;

public sealed record DeleteItemStockCommand(string UserId, DeleteItemStockDto Dto) : CommandQueryBase<Result>(UserId);