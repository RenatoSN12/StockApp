using StockApp.Domain.Enums;

namespace StockApp.Application.DTOs.Requests.Products;

public sealed class CreateProductDto
{
    public string CustomId { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public EStatus Status { get; set; }
    public string ImageUrl { get; set; } = string.Empty;
    public long CategoryId { get; set; }
}