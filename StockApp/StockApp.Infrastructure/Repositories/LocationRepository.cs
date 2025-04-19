using Microsoft.EntityFrameworkCore;
using StockApp.Domain.Abstractions;
using StockApp.Domain.Entities;
using StockApp.Domain.Repositories;
using StockApp.Domain.Specification.Locations;
using StockApp.Infrastructure.Data;

namespace StockApp.Infrastructure.Repositories;

public class LocationRepository(AppDbContext context) : ILocationRepository
{
    public async Task<IEnumerable<Location>> GetAllLocationsAsync(GetAllLocationsSpecification specification,
        CancellationToken cancellationToken = default)
        => await context.Locations.Where(specification.ToExpression()).AsNoTracking().ToListAsync(cancellationToken);

    public async Task AddAsync(Location location, CancellationToken cancellationToken = default)
        => await context.Locations.AddAsync(location, cancellationToken);

    public async Task<int> GetTotalCount(Specification<Location> specification, CancellationToken cancellationToken = default)
        => await context.Locations.CountAsync(specification.ToExpression(), cancellationToken);
}