using StockApp.Domain.Enums;

namespace StockApp.Domain.DTOs.Responses.Products;

public sealed record ResumeProductDto(string CustomId, string? ImageUrl, string Title, EStatus Status);