using System.Linq.Expressions;
using StockApp.Domain.Abstractions;
using StockApp.Domain.Entities;

namespace StockApp.Domain.Specification.Products;

public record GetProductByIdSpecification(long ProductId, string UserId) : Specification<Product>
{
    public override Expression<Func<Product, bool>> ToExpression()
        => product => product.Id == ProductId && product.UserId == UserId;
}