namespace StockApp.Application.DTOs.Requests.ProductStock;

public sealed record CreateProductStockDto(long ProductId, long LocationId, long MaxQuantity, long MinQuantity);