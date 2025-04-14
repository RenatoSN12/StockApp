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
            product.ImageUrl,
            product.IsActive,
            product.CreatedAt,
            product.UpdatedAt,
            product.CategoryId
        );
    }
}