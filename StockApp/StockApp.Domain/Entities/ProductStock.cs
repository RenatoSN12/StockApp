using StockApp.Domain.Abstractions;
using StockApp.Shared;

namespace StockApp.Domain.Entities;
public class ProductStock : Entity
{
    public long ItemId { get; private set; }
    public Product Product { get; private set; } = null!;

    public Location Location { get; private set; } = null!;
    public long LocationId { get; private set; }
    
    public long Quantity { get; private set; }
    public long MinimumStockLevel  { get; private set; }
    public long MaximumStockLevel { get; private set; }
    public DateTime LastUpdatedDate { get; private set; }
    
    private ProductStock() { }

    private ProductStock(
        string userId,
        long itemId,
        long locationId,
        long minimumStockLevel,
        long maximumStockLevel)
    {
        UserId = userId;
        ItemId = itemId;
        LocationId = locationId;
        Quantity = 0;
        MinimumStockLevel = minimumStockLevel;
        MaximumStockLevel = maximumStockLevel;
        LastUpdatedDate = DateTime.Now;
    }

    public static Result<ProductStock> Create(string userId,long itemId, long locationId, long minimumStockLevel, long maximumStockLevel)
    {
        var productStock = new ProductStock(userId, itemId, locationId, minimumStockLevel, maximumStockLevel);
        return Result<ProductStock>.Success(productStock);
    }
    
}