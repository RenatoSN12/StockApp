using StockApp.Application.DTOs.Requests.Locations;
using StockApp.Application.DTOs.Responses.Location;
using StockApp.Shared;

namespace StockApp.Web.Services.Abstractions;

public interface ILocationService
{
    Task<Result<PagedResponse<List<LocationDto>>>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<Result<LocationDto>> CreateAsync(CreateLocationDto request, CancellationToken cancellationToken = default);
}