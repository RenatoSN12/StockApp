using System.Linq.Expressions;
using StockApp.Domain.Abstractions;
using StockApp.Domain.Entities;

namespace StockApp.Domain.Specification.Categories;

public sealed record GetAllCategoriesByUserSpecification(string Email) : Specification<Category>
{
    public override Expression<Func<Category, bool>> ToExpression()
        => category => category.UserId == Email;
}