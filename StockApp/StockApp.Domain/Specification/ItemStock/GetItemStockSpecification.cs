using System.Linq.Expressions;
using StockApp.Domain.Abstractions;

namespace StockApp.Domain.Specification.ItemStock;

public sealed record GetItemStockSpecification(long ProductId, long LocationId) : Specification<Entities.ItemStock>
{
    public override Expression<Func<Entities.ItemStock, bool>> ToExpression()
        => stock => stock.LocationId == LocationId && stock.ItemId == ProductId;
}