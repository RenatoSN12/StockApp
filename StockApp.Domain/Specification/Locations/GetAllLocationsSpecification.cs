using System.Linq.Expressions;
using StockApp.Domain.Abstractions;
using StockApp.Domain.Entities;

namespace StockApp.Domain.Specification.Locations;

public sealed record GetAllLocationsSpecification(string UserId) : Specification<Location>
{
    public override Expression<Func<Location, bool>> ToExpression()
        => location => location.UserId == UserId;
}