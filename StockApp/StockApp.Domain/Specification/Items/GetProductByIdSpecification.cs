using System.Linq.Expressions;
using StockApp.Domain.Abstractions;
using StockApp.Domain.Entities;

namespace StockApp.Domain.Specification.Items;

public sealed record GetProductByIdSpecification(long Id) : Specification<Product>
{
    public override Expression<Func<Product, bool>> ToExpression()
        => item => item.Id == Id;    
}