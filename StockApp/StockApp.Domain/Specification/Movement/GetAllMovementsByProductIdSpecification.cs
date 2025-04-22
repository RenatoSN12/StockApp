using System.Linq.Expressions;
using StockApp.Domain.Abstractions;

namespace StockApp.Domain.Specification.Movement;

public sealed record GetAllMovementsByProductIdSpecification(string UserId, long ProductId) : Specification<Entities.Movement>
{
    public override Expression<Func<Entities.Movement, bool>> ToExpression()
        => x => x.UserId == UserId && x.ProductId == ProductId;
}