using StockApp.Domain.Abstractions;
using StockApp.Domain.Entities;

namespace StockApp.Domain.Repositories.Products;

public interface IProductRepository : IRepository<Product>
{
    Task CreateAsync(Product product, CancellationToken cancellationToken = default);
    Task<Product?> GetByCustomIdAsync(Specification<Product> specification, CancellationToken cancellationToken = default);
    Task<List<Product>?> GetAllAsync(Specification<Product> specification,
        int pageNumber, int pageSize, CancellationToken cancellationToken = default);
}