namespace StockApp.Application.DTOs.Responses.ItemStock;

public sealed record ItemStockDto(long ProductId, long LocationId, long Quantity, long MinQuantity, long MaxQuantity);