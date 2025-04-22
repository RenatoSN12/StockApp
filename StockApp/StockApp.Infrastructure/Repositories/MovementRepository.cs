using Microsoft.EntityFrameworkCore;
using StockApp.Domain.Abstractions;
using StockApp.Domain.Entities;
using StockApp.Domain.Repositories;
using StockApp.Infrastructure.Data;

namespace StockApp.Infrastructure.Repositories;

public class MovementRepository(AppDbContext context) : IMovementRepository
{
    public async Task AddAsync(Movement movement, CancellationToken cancellationToken = default)
        => await context.AddAsync(movement, cancellationToken);

    public async Task<List<Movement>> GetAllMovementsByProductCustomId(Specification<Movement> specification,
        int pageNumber, int pageSize,
        CancellationToken cancellationToken = default)
    {
        var query = context.Movements
            .Where(specification.ToExpression())
            .OrderByDescending(x=> x.MovementDate);
        
        var movements = await query
            .Skip(pageSize * (pageNumber - 1))
            .Take(pageSize)
            .ToListAsync(cancellationToken);
        
        return movements;
    }
}
