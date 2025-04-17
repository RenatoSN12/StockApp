using StockApp.Domain.Abstractions;
using StockApp.Domain.Enums;
using StockApp.Shared;

namespace StockApp.Domain.Entities;

public class Product : Entity
{
    public string CustomId { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public string? Description { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public EStatus Status { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime UpdatedAt { get; set; } = DateTime.Now;
    public long CategoryId { get; set; }
    public Category? Category { get; set; } = null!;
    public string? ImageUrl { get; set; }
    public ICollection<ItemStock> Inventories { get; set; } = [];

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

    public static Result<Product> Create(string userId, string customId, string title, string description,
        decimal price, EStatus isActive, long categoryId,
        string? imageUrl)
    {
        var product = new Product(userId, customId, title, description, price, isActive, categoryId, imageUrl);

        // Por enquanto não há validações de negócio.
        return Result<Product>.Success(product);
    }

    private Product()
    {
    }
}