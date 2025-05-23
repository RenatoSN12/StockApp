using StockApp.Domain.Abstractions;
using StockApp.Domain.Entities;

namespace StockApp.Domain.Repositories;

public interface IProductStockRepository
{
    void Delete(ProductStock productStock);
    Task<ProductStock?> GetProductStock(Specification<ProductStock> specification, bool asNoTracking, CancellationToken cancellationToken = default);
    Task Insert(ProductStock productStock, CancellationToken cancellationToken = default);
}