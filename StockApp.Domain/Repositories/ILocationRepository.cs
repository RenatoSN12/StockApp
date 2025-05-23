using StockApp.Domain.Abstractions;
using StockApp.Domain.Entities;
using StockApp.Domain.Specification.Locations;

namespace StockApp.Domain.Repositories;

public interface ILocationRepository : IRepository<Location>
{
    Task<IEnumerable<Location>> GetAllLocationsAsync(GetAllLocationsSpecification specification, CancellationToken cancellationToken = default);
    Task<Location?> GetLocationAsync(Specification<Location> specification, bool asNoTracking ,CancellationToken cancellationToken = default);
    Task AddAsync(Location location, CancellationToken cancellationToken = default);
}