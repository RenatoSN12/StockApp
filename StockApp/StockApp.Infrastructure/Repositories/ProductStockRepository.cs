using Microsoft.EntityFrameworkCore;
using StockApp.Domain.Abstractions;
using StockApp.Domain.Entities;
using StockApp.Domain.Repositories;
using StockApp.Infrastructure.Data;

namespace StockApp.Infrastructure.Repositories;

public class ProductStockRepository(AppDbContext context) : IProductStockRepository
{
    public void Delete(ProductStock productStock)
        => context.ItemStocks.Remove(productStock);

    public async Task<ProductStock?> GetProductStock(Specification<ProductStock> specification, bool asNoTracking,
        CancellationToken cancellationToken = default)
    {
        var query = context.ItemStocks
            .Where(specification.ToExpression())
            .AsQueryable();
        
        if(asNoTracking)
            query = query.AsNoTracking();
        
        return await query
            .Include(p => p.Product)
            .Include(l=> l.Location)
            .FirstOrDefaultAsync(cancellationToken);
    }

    public async Task Insert(ProductStock productStock, CancellationToken cancellationToken = default)
        => await context.ItemStocks.AddAsync(productStock, cancellationToken);
}