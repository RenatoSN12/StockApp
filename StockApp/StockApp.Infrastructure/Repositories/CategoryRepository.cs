using Microsoft.EntityFrameworkCore;
using StockApp.Domain.Abstractions;
using StockApp.Domain.Entities;
using StockApp.Domain.Repositories;
using StockApp.Domain.Specification.Categories;
using StockApp.Infrastructure.Data;

namespace StockApp.Infrastructure.Repositories;

public class CategoryRepository(AppDbContext context) : ICategoryRepository
{
    public async Task<List<Category>> GetAllByUserAsync(GetAllCategoriesByUserSpecification specification, int pageNumber, int pageSize,
        CancellationToken cancellationToken = default)
    {
        var query = context.Categories.AsNoTracking().Where(specification.ToExpression())
            .OrderBy(x => x.Id);

        var categories = await query
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync(cancellationToken);

        return categories;
    }

    public async Task<Category?> GetById(GetCategoryByIdSpecification specification, bool asNoTracking, CancellationToken cancellationToken)
    {
        var query = context.Categories.AsQueryable();
        if (asNoTracking)
            query = query.AsNoTracking();
        
        return await query
            .Where(specification.ToExpression())
            .FirstOrDefaultAsync(cancellationToken);
    }
    public async Task AddAsync(Category category, CancellationToken cancellationToken)
        => await context.Categories.AddAsync(category, cancellationToken);

    public void Remove(Category category)
        => context.Categories.Remove(category);
    
    public async Task<int> GetTotalCount(Specification<Category> specification, CancellationToken cancellationToken = default)
        => await context.Categories.AsNoTracking().Where(specification.ToExpression()).CountAsync(cancellationToken);
    public void Update(Category category)
        => context.Categories.Update(category);
}