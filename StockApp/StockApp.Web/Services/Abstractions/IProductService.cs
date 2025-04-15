using MudBlazor;
using StockApp.Application.DTOs.Responses.Products;
using StockApp.Shared;

namespace StockApp.Web.Services.Abstractions;

public interface IProductService
{
    Task<Result<PagedResponse<List<ResumeProductDto>>>> GetAllAsync(CancellationToken cancellationToken = default);
}