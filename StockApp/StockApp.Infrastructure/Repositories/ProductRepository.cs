using Microsoft.EntityFrameworkCore;
using StockApp.Domain.Abstractions;
using StockApp.Domain.Entities;
using StockApp.Domain.Repositories;
using StockApp.Infrastructure.Data;

namespace StockApp.Infrastructure.Repositories;

public class ProductRepository(AppDbContext context) : IProductRepository
{
    public async Task<int> GetTotalCount(Specification<Product> specification,
        CancellationToken cancellationToken = default)
        => await context.Products.AsNoTracking().Where(specification.ToExpression()).CountAsync(cancellationToken);

    public async Task AddAsync(Product product, CancellationToken cancellationToken = default)
        => await context.Products.AddAsync(product, cancellationToken);

    public void Update(Product product)
        => context.Products.Update(product);

    public async Task<Product?> GetByCustomIdAsync(Specification<Product> specification, bool asNoTracking,
        CancellationToken cancellationToken = default)
    {
        var query = context.Products.AsQueryable();
        if(asNoTracking)
            query = query.AsNoTracking();
        
        return await query.Include(x=>x.Inventories).ThenInclude(i=> i.Location).FirstOrDefaultAsync(specification.ToExpression(), cancellationToken);
    }

    public async Task<List<Product>?> GetAllAsync(Specification<Product> specification, int pageNumber, int pageSize,
        CancellationToken cancellationToken = default)
    {
        var query = context.Products.Where(specification.ToExpression()).OrderBy(x => x.CustomId);

        var products = await query
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .AsNoTracking()
            .ToListAsync(cancellationToken);
        
        return products;
    }
}