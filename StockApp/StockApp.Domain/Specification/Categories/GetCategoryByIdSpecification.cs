using System.Linq.Expressions;
using StockApp.Domain.Abstractions;
using StockApp.Domain.Entities;

namespace StockApp.Domain.Specification.Categories;

public sealed record GetCategoryByIdSpecification(string UserId, long Id) : Specification<Category>
{
    public override Expression<Func<Category, bool>> ToExpression()
        => category => category.Id == Id && category.UserId == UserId;
}