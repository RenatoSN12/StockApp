using StockApp.Domain.Abstractions;
using StockApp.Domain.Entities;

namespace StockApp.Domain.Repositories;

public interface IMovementRepository
{
    Task AddAsync(Movement movement, CancellationToken cancellationToken = default);
    Task<List<Movement>> GetAllMovementsByProductCustomId(Specification<Movement> specification, int pageNumber, int pageSize, CancellationToken cancellationToken = default);
}