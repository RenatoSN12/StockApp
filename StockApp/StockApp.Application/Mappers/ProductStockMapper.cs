using StockApp.Application.DTOs.Responses.ProductStock;
using StockApp.Domain.Entities;

namespace StockApp.Application.Mappers;

public static class ProductStockMapper
{
    public static ProductStockDto ToDto(this ProductStock productStock) => new(
        productStock.ItemId, 
        productStock.LocationId,
        productStock.Location.Title,        
        productStock.Quantity,
        productStock.MinimumStockLevel,
        productStock.MaximumStockLevel
        );
}