using Microsoft.EntityFrameworkCore;
using StockApp.Domain.Abstractions;
using StockApp.Domain.Abstractions.Interfaces;
using StockApp.Domain.Entities;
using StockApp.Domain.Repositories.Products;
using StockApp.Infrastructure.Data;

namespace StockApp.Infrastructure.Repositories;

public class ProductRepository(AppDbContext context) : IProductRepository
{
    public async Task<int> GetTotalCount(ISpecification<Product> specification,
        CancellationToken cancellationToken = default)
        => await context.Products.AsNoTracking().Where(specification.ToExpression()).CountAsync(cancellationToken);

    public async Task CreateAsync(Product product, CancellationToken cancellationToken = default)
        => await context.Products.AddAsync(product, cancellationToken);
    
    public Task<Product?> GetByIdAsync(Specification<Product> specification,
        CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
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