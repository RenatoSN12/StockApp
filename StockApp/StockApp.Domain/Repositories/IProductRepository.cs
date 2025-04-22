using StockApp.Domain.Abstractions;
using StockApp.Domain.Entities;

namespace StockApp.Domain.Repositories;

public interface IProductRepository : IRepository<Product>
{
    Task AddAsync(Product product, CancellationToken cancellationToken = default);
    void Update(Product product);
    Task<Product?> GetByCustomIdAsync(Specification<Product> specification, bool asNoTracking, CancellationToken cancellationToken = default);
    Task<List<Product>?> GetAllAsync(Specification<Product> specification,
        int pageNumber, int pageSize, CancellationToken cancellationToken = default);
}