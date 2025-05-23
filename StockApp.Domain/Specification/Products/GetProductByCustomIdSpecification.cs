using System.Linq.Expressions;
using StockApp.Domain.Abstractions;
using StockApp.Domain.Entities;

namespace StockApp.Domain.Specification.Products;

public sealed record GetProductByCustomIdSpecification(string CustomId, string UserId) : Specification<Product>
{
    public override Expression<Func<Product, bool>> ToExpression()
        => item => item.CustomId == CustomId && item.UserId == UserId;    
}