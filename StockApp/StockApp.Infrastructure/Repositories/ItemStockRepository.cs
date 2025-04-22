using Microsoft.EntityFrameworkCore;
using StockApp.Domain.Abstractions;
using StockApp.Domain.Entities;
using StockApp.Domain.Repositories;
using StockApp.Infrastructure.Data;

namespace StockApp.Infrastructure.Repositories;

public class ItemStockRepository(AppDbContext context) : IItemStockRepository
{
    public void Delete(ItemStock itemStock)
        => context.ItemStocks.Remove(itemStock);

    public async Task<ItemStock?> GetItemStock(Specification<ItemStock> specification, CancellationToken cancellationToken = default)
        => await context.ItemStocks.Where(specification.ToExpression()).FirstOrDefaultAsync(cancellationToken);

    public async Task Insert(ItemStock itemStock, CancellationToken cancellationToken = default)
        => await context.ItemStocks.AddAsync(itemStock, cancellationToken);
}