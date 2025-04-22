namespace StockApp.Application.DTOs.Requests.ItemStock;

public sealed record DeleteItemStockDto(long ProductId, long LocationId);