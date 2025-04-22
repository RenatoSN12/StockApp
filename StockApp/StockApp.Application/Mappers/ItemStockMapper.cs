using StockApp.Application.DTOs.Responses.ItemStock;
using StockApp.Domain.Entities;

namespace StockApp.Application.Mappers;

public static class ItemStockMapper
{
    public static ItemStockDto ToDto(this ItemStock itemStock) => new(
        itemStock.ItemId, 
        itemStock.LocationId,
        itemStock.Quantity,
        itemStock.MinimumStockLevel,
        itemStock.MaximumStockLevel
        );
}