using StockApp.Domain.Abstractions;
using StockApp.Domain.Enums;
using StockApp.Shared;

namespace StockApp.Domain.Entities;

public class Product : Entity
{
    public string CustomId { get; private set; } = string.Empty;
    public string Title { get; private set; } = string.Empty;
    public string? Description { get; private set; } = string.Empty;
    public decimal Price { get; private set; }
    public EStatus Status { get; private set; }
    public DateTime CreatedAt { get; private set; } = DateTime.Now;
    public DateTime UpdatedAt { get; private set; } = DateTime.Now;
    public long CategoryId { get; private set; }
    public Category? Category { get; private set; } = null!;
    public string? ImageUrl { get; private set; }
    public ICollection<ItemStock> Inventories { get; private set; } = [];

    private Product(string userId, string customId, string title, string description, decimal price, EStatus status,
        long categoryId,
        string? imageUrl)
    {
        UserId = userId;
        CustomId = customId;
        Title = title;
        Description = description;
        Price = price;
        Status = status;
        CategoryId = categoryId;
        ImageUrl = imageUrl;
        
        var now = DateTime.Now;
        UpdatedAt = now;
        CreatedAt = now;
    }

    public Result<Product> Update(string title, string description, decimal price, EStatus status, long categoryId, string? imageUrl)
    {
        Title = title;
        Description = description;
        Price = price;
        Status = status;
        CategoryId = categoryId;
        ImageUrl = imageUrl;
        UpdatedAt = DateTime.Now;
        
        return Result.Success(this);
    }

    public static Result<Product> Create(string userId, string customId, string title, string description,
        decimal price, EStatus isActive, long categoryId,
        string? imageUrl)
    {
        var product = new Product(userId, customId, title, description, price, isActive, categoryId, imageUrl)
            {
                Status = EStatus.Active
            };

        // Por enquanto não há validações de negócio.
        return Result<Product>.Success(product);
    }

    private Product()
    {
    }
}