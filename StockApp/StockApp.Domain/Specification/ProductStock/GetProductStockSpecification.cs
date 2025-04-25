using System.Linq.Expressions;
using StockApp.Domain.Abstractions;

namespace StockApp.Domain.Specification.ProductStock;

public sealed record GetProductStockSpecification(string UserId, long ProductId, long LocationId) : Specification<Entities.ProductStock>
{
    public override Expression<Func<Entities.ProductStock, bool>> ToExpression()
        => stock => stock.LocationId == LocationId && stock.ItemId == ProductId && stock.UserId == UserId;
}