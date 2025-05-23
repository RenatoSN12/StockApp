namespace StockApp.Application.DTOs.Requests.Categories;

public sealed class UpdateCategoryDto
{
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
}