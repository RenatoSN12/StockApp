using MudBlazor;
using StockApp.Application.DTOs.Requests.Products;
using StockApp.Application.DTOs.Responses.Products;
using StockApp.Domain.Entities;
using StockApp.Shared;

namespace StockApp.Web.Services.Abstractions;

public interface IProductService
{
    Task<Result<PagedResponse<List<ResumeProductDto>>>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<Result<ProductDto>> GetByCustomId(string customId,CancellationToken cancellationToken = default);
    Task<Result<ProductDto>> CreateAsync(CreateProductDto productDto, CancellationToken cancellationToken = default);
    Task<Result<ProductDto>> UpdateAsync(ProductDto productDto, CancellationToken cancellationToken = default);
    Task<Result<ProductDto>> InactivateAsync(long productId, CancellationToken cancellationToken = default);
    Task<Result<ProductDto>> ActivateAsync(long productId, CancellationToken cancellationToken = default);
}