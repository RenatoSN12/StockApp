using StockApp.Domain.Abstractions;
using StockApp.Domain.Abstractions.Interfaces;

namespace StockApp.Domain.Repositories;

public interface IRepository<T> where T : Entity
{
    Task<int> GetTotalCount(Specification<T> specification, CancellationToken cancellationToken = default);
}