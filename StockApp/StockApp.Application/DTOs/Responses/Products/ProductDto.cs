using StockApp.Domain.Enums;

namespace StockApp.Application.DTOs.Responses.Products;

public sealed class ProductDto
{
    public ProductDto(string customId,
        string title,
        decimal price,
        string imageUrl,
        string description,        
        EStatus status,
        DateTime createdDate,
        DateTime updatedDate,
        long categoryId)
    {
        CustomId = customId;
        Title = title;
        Description = description;
        Price = price;
        ImageUrl = imageUrl;
        Status = status;
        CreatedDate = createdDate;
        UpdatedDate = updatedDate;
        CategoryId = categoryId;
    }
    
    public ProductDto(){}
    public string CustomId { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public string ImageUrl { get; set; }
    public EStatus Status { get; set; } = EStatus.Active;
    public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedDate { get; set; } = DateTime.UtcNow;
    public long CategoryId { get; set; }
}