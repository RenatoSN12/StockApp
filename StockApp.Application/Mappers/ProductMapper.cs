using StockApp.Application.DTOs.Responses.Location;
using StockApp.Application.DTOs.Responses.Products;
using StockApp.Application.DTOs.Responses.ProductStock;
using StockApp.Domain.Entities;

namespace StockApp.Application.Mappers;

public static class ProductMapper
{
    public static ProductDto ToDto(Product product)
    {
        return new ProductDto(
            product.CustomId,
            product.Id,
            product.Title,
            product.Price,
            product.Description,
            product.ImageUrl,
            product.Status,
            product.CreatedAt,
            product.UpdatedAt,
            product.CategoryId,
            product.Inventories
                .Select(x => x.ToDto())
                .ToList()
        );
    }
    private static ResumeProductDto ToResumedDto(Product product)
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