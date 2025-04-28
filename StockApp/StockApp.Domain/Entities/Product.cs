using System.Runtime.CompilerServices;
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
    public ICollection<ProductStock> Inventories { get; private set; } = [];

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

    public Result<Product> Update(string title, string description, decimal price, EStatus status, long categoryId,
        string? imageUrl)
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

    public Result<Product> Inactive()
    {
        if (Status == EStatus.Inactive)
            return Result.Failure<Product>(new Error("400", "O produto informado já está inativo."));

        var inventoriesWithQuantity = Inventories
            .Where(i => i.Quantity > 0)
            .Select(l=> l.Location.Title)
            .ToList();

        if (inventoriesWithQuantity.Count > 0)
            return Result.Failure<Product>(new Error("400",
                $"Este produto ainda possui quantidade ativa no(s) estoque(s): {string.Join(", ", inventoriesWithQuantity)} — para cancelar o produto, zere as quantidades de estoque associadas a ele primeiro."
            ));

        Status = EStatus.Inactive;
        Inventories.Clear();
        
        return Result.Success(this);
    }
    
    public Result<Product> Activate()
    {
        if (Status == EStatus.Active)
            return Result.Failure<Product>(new Error("400", "O produto informado já está ativo."));

        Status = EStatus.Active;
        return Result.Success(this);
    }

    private Product()
    {
    }
}