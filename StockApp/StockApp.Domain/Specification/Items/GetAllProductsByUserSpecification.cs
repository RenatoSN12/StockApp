using System.Linq.Expressions;
using StockApp.Domain.Abstractions;
using StockApp.Domain.Entities;

namespace StockApp.Domain.Specification.Items;

public sealed record GetAllProductsByUserSpecification(string UserId) : Specification<Product>
{
    public override Expression<Func<Product, bool>> ToExpression()
        => product => product.UserId == UserId;
}