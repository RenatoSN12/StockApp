using StockApp.Domain.Abstractions;
using StockApp.Domain.Entities;

namespace StockApp.Domain.Repositories;

public interface IItemStockRepository
{
    void Delete(ItemStock itemStock);
    Task<ItemStock?> GetItemStock(Specification<ItemStock> specification, CancellationToken cancellationToken = default);
    Task Insert(ItemStock itemStock, CancellationToken cancellationToken = default);
}