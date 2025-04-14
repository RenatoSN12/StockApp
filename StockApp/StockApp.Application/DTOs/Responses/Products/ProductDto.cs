using StockApp.Domain.Enums;

namespace StockApp.Application.DTOs.Responses.Products;

public sealed record ProductDto(
    string CustomId,
    string Title,
    decimal Price,
    string? ImageUrl,
    EStatus Status,
    DateTime CreatedDate,
    DateTime UpdatedDate,
    long CategoryId);