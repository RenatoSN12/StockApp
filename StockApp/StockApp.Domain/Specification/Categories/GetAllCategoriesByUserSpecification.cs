using System.Linq.Expressions;
using StockApp.Domain.Abstractions;
using StockApp.Domain.Entities;
using StockApp.Domain.Enums;

namespace StockApp.Domain.Specification.Categories;

public class GetAllCategoriesByUserSpecification(string email) : Specification<Category>
{
    public override Expression<Func<Category, bool>> ToExpression()
        => category => category.UserId == email && category.Status == EStatus.Active;    
}