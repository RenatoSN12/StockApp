using StockApp.Application.DTOs.Responses.Products;
using StockApp.Domain.Entities;

namespace StockApp.Application.Mappers;

public static class ProductMapper
{
    public static ProductDto ToDto(Product product)
    {
        return new ProductDto(
            product.CustomId,
            product.Title,
            product.Price,
            product.Description,
            product.ImageUrl,
            product.Status,
            product.CreatedAt,
            product.UpdatedAt,
            product.CategoryId
        );
    }
    public static ResumeProductDto ToResumedDto(Product product)
    {
        return new ResumeProductDto(
            product.CustomId,
            product.ImageUrl,
            product.Title,
            product.Status
        );
    }
    
    public static List<ResumeProductDto> ToResumedDtoList(List<Product> products)
    {
        return products.Select(ToResumedDto).ToList();
    }
}