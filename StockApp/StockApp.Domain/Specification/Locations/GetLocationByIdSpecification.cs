using System.Linq.Expressions;
using StockApp.Domain.Abstractions;
using StockApp.Domain.Entities;

namespace StockApp.Domain.Specification.Locations;

public record GetLocationByIdSpecification(long LocationId, string UserId) : Specification<Location>
{
    public override Expression<Func<Location, bool>> ToExpression()
        => location => location.Id == LocationId && location.UserId == UserId;
}