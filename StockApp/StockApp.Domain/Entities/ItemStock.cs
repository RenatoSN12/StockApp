using StockApp.Domain.Abstractions;

namespace StockApp.Domain.Entities;
public class ItemStock : Entity
{
    public long ItemId { get; set; }
    public Product Product { get; set; } = null!;

    public Location Location { get; set; } = null!;
    public long LocationId { get; set; }
    
    public long Quantity { get; set; }
    public long MinimumStockLevel  { get; set; }
    public long MaximumStockLevel { get; set; }
    public DateTime LastUpdatedDate { get; set; }
}