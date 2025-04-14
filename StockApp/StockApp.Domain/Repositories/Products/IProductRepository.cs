using StockApp.Domain.Abstractions;
using StockApp.Domain.Entities;
using StockApp.Shared;

namespace StockApp.Domain.Repositories.Products;

public interface IProductRepository : IRepository<Product>
{
    Task CreateAsync(Product product, CancellationToken cancellationToken = default);
    Task<Product?> GetByIdAsync(Specification<Product> specification, CancellationToken cancellationToken = default);
    Task<List<Product>?> GetAllAsync(Specification<Product> specification,
        int pageNumber, int pageSize, CancellationToken cancellationToken = default);
}