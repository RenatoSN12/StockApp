using StockApp.Domain.Abstractions.Interfaces;

namespace StockApp.Infrastructure.Data;

public class UnitOfWork(AppDbContext context) : IUnitOfWork
{
    public async Task CommitAsync(CancellationToken cancellationToken) 
        => await context.SaveChangesAsync(cancellationToken);
}