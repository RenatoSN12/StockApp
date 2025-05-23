using StockApp.Domain.Enums;

namespace StockApp.Application.DTOs.Responses.Products;

public sealed record ResumeProductDto(string CustomId, string? ImageUrl, string Title, EStatus Status);