namespace StockApp.Application.DTOs.Requests.ProductStock;

public sealed record DeleteProductStockDto(long ProductId, long LocationId);