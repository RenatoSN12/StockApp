using System.Linq.Expressions;
using StockApp.Domain.Abstractions;
using StockApp.Domain.Entities;

namespace StockApp.Domain.Specification.Categories;

public sealed class GetCategoryByIdSpecification(string userId, int id) : Specification<Category>
{
    public override Expression<Func<Category, bool>> ToExpression()
        => category => category.Id == id && category.UserId == userId;
}