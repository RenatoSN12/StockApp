using StockApp.Domain.Entities;
using StockApp.Domain.Specification.Categories;

namespace StockApp.Domain.Repositories;

public interface ICategoryRepository : IRepository<Category>
{
    Task<List<Category>> GetAllByUserAsync(GetAllCategoriesByUserSpecification specification, int pageNumber,
        int pageSize, CancellationToken cancellationToken = default);
    Task<Category?> GetById(GetCategoryByIdSpecification specification, bool asNoTracking,CancellationToken cancellationToken);
    Task AddAsync(Category category,CancellationToken cancellationToken);
    void Remove(Category category,CancellationToken cancellationToken);
}