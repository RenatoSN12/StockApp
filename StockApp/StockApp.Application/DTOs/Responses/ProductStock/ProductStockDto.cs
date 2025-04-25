namespace StockApp.Application.DTOs.Responses.ProductStock;

public sealed record ProductStockDto(long ProductId, long LocationId, string LocationTitle, long Quantity, long MinQuantity, long MaxQuantity);