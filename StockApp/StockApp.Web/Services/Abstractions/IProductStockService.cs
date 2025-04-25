using StockApp.Application.DTOs.Requests.ProductStock;
using StockApp.Application.DTOs.Responses.ProductStock;
using StockApp.Shared;

namespace StockApp.Web.Services.Abstractions;

public interface IProductStockService
{
    Task<Result<ProductStockDto>> AddAsync(CreateProductStockDto request, CancellationToken cancellationToken = default);
    Task<Result<string>> DeleteAsync(DeleteProductStockDto request, CancellationToken cancellationToken = default);
}