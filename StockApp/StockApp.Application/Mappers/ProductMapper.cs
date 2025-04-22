using StockApp.Application.DTOs.Responses.Location;
using StockApp.Application.DTOs.Responses.Products;
using StockApp.Domain.Entities;

namespace StockApp.Application.Mappers;

public static class ProductMapper
{
    public static ProductDto ToDto(Product product)
    {
        var locations = product.Inventories
            .Select(x => new ProductLocationDto(x.LocationId, x.Location.Title, x.Quantity))
            .ToList();
            
        return new ProductDto(
            product.CustomId,
            product.Title,
            product.Price,
            product.Description,
            product.ImageUrl,
            product.Status,
            product.CreatedAt,
            product.UpdatedAt,
            product.CategoryId,
            locations
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