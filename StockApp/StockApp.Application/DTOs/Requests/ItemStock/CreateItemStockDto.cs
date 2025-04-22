namespace StockApp.Application.DTOs.Requests.ItemStock;

public sealed record CreateItemStockDto(long ProductId, long LocationId, long MaxQuantity, long MinQuantity);